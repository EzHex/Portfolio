using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Serveris
{
    public class Task
    {
        
        /// <summary>
        /// Reads data from files
        /// </summary>
        /// <param name="allFiles">List of data file paths</param>
        /// <returns>List of registers</returns>
        public static List<DayRegister> ReadFiles(string[] allFiles)
        {
            List<DayRegister> registers = new List<DayRegister>();
            foreach (string item in allFiles)
            {
                List<Day> days = new List<Day>();

                var lines = File.ReadLines(item)
                    .Select(l => l.Split(';')).ToList();

                DateTime date = DateTime.Parse(lines[0][0]);
                string ip = lines[0][1];
                for (int i = 1; i < lines.Count; i++)
                {
                    DateTime time = DateTime.Parse(lines[i][0]);
                    string pcIP = lines[i][1];
                    string path = lines[i][2];

                    Day day = new Day(pcIP, path, time);

                    days.Add(day);
                }

                DayRegister dayRegister = new DayRegister(date, days, ip);
                registers.Add(dayRegister);
            }
            return registers;
        }
        /// <summary>
        /// Reads data from server file
        /// </summary>
        /// <param name="path">Server file path</param>
        /// <returns>List of servers</returns>
        public static List<Server> ReadFile(string path)
        {
            List<Server> servers = new List<Server>();

            var lines = File.ReadLines(path)
                    .Select(l => l.Split(';')).ToList();
            lines.ForEach(line =>
            {
                Server server = new Server(line[0], line[1]);
                servers.Add(server);
            });

            return servers;
        }
        /// <summary>
        /// Converts registers for txt file.
        /// </summary>
        public static List<string> LinesForTxt(string header, List<DayRegister> registers)
        {

            List<string> result = new List<string>();
            result.Add(Punctuation(30));
            result.Add(header);
            result.Add(Punctuation(30));
            result.Add("\n");

            registers.ForEach(reg =>
            {
                int max = 0;
                result.Add("");
                result.Add(string.Format("| {0,-10} | {1, -16} |", "Data", "Serverio IP"));
                result.Add("");
                result.Add(reg.ToString());
                result.Add("");
                result.Add(string.Format("| {0,-10} | {1, -16} | {2}", "Laikas", "IP adresas", "Adresas"));
                result.Add("");
                foreach (var item in reg)
                {
                    if (item.ToString().Length > max) max = item.ToString().Length;
                    result.Add(item.ToString());
                }
                result.Add("");
                result = result.Select(line => string.IsNullOrEmpty(line) ? Punctuation(max) : line).ToList();
                result.Add("\n");
            });



            return result;
        }
        /// <summary>
        /// Converts server list for txt.
        /// </summary>
        public static List<string> LinesForTxt(string header, List<Server> servers)
        {
            int max = 0;
            List<string> result = new List<string>();

            result.Add("");
            result.Add(header);
            result.Add("");
            result.Add(string.Format("| {0, -16} | {1} ", "IP adresas", "Adresas"));
            result.Add("");
            servers.ForEach(server =>
            {
                if (server.ToString().Length > max) max = server.ToString().Length;
                result.Add(server.ToString());
            });
            result.Add("");

            result = result.Select(line => string.IsNullOrEmpty(line) ? Punctuation(max) : line).ToList();
            result.Add("\n");

            return result;
        }
        /// <summary>
        /// Converts list of days for txt
        /// </summary>
        public static List<string> LinesForTxt(string header, List<Day> days)
        {
            int max = 0;
            List<string> result = new List<string>();

            result.Add("");
            result.Add(header);
            result.Add("");
            result.Add(string.Format("| {0,-10} | {1, -16} | {2}", "Laikas", "IP adresas", "Adresas"));
            result.Add("");

            days.ForEach(day =>
            {
                if (day.ToString().Length > max) max = day.ToString().Length;
                result.Add(day.ToString());
            });
            result.Add("");

            result = result.Select(line => string.IsNullOrEmpty(line) ? Punctuation(max) : line).ToList();

            return result;
        }
        /// <summary>
        /// Dynamic table borders for txt file.
        /// </summary>
        /// <param name="a">length</param>
        private static string Punctuation(int a)
        {
            return new string('-', a);
        }
        /// <summary>
        /// Looping through all registers
        /// </summary>
        public static void CreateTable(List<DayRegister> registers, Panel panel)
        {
            registers.ForEach(reg => CreateTableForRegister(reg, panel));
        }
        /// <summary>
        /// Creating table to show data from files in tables.
        /// </summary>
        /// <param name="register"> Data register</param>
        /// <param name="panel1">Panel to put table</param>
        private static void CreateTableForRegister(DayRegister register, Panel panel1)
        {
            Table table = new Table();

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            Label label = new Label();
            label.Text = "Data: " + register.Date.ToString("d");
            label.CssClass = "labelForTable";

            row = new TableRow();
            cell = new TableCell();
            cell.Text = "Laikas";
            cell.ColumnSpan = 1;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "IP adresas";
            cell.ColumnSpan = 1;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Puslapis";
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);

            row.CssClass = "firstrow";

            table.Rows.Add(row);

            for (int i = 0; i < register.Count(); i++)
            {
                row = new TableRow();
                Day day = register.GetDay(i);

                cell = new TableCell();
                cell.Text = day.Time.ToString("HH:mm:ss");
                cell.ColumnSpan = 1;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = day.IP;
                cell.ColumnSpan = 1;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = day.Path;
                cell.ColumnSpan = 3;
                row.Cells.Add(cell);

                table.Rows.Add(row);
            }

            table.CssClass = "styled-table";
            panel1.Controls.Add(label);
            panel1.Controls.Add(table);
            panel1.Controls.Add(new LiteralControl("<br />"));
        }
        /// <summary>
        /// Creating table to show data from file in table.
        /// </summary>
        /// <param name="servers">Servers list</param>
        /// <param name="panel1">Panel to put table</param>
        public static void CreateTableForServer(List<Server> servers, Panel panel1)
        {
            Table table = new Table();
            Label label = new Label();
            label.Text = "Serverių sąrašas";
            label.CssClass = "labelForTable";

            TableRow row = new TableRow();
            TableCell cell = new TableCell();

            cell.Text = "IP adresas";
            cell.ColumnSpan = 1;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = "Puslapis";
            cell.ColumnSpan = 3;
            row.Cells.Add(cell);

            row.CssClass = "firstrow";
            table.Rows.Add(row);

            servers.ForEach(server =>
            {
                row = new TableRow();
                cell = new TableCell();

                cell.Text = server.IP;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = server.Path;
                row.Cells.Add(cell);

                table.Rows.Add(row);
            });

            table.CssClass = "styled-table";
            panel1.Controls.Add(label);
            panel1.Controls.Add(table);
            panel1.Controls.Add(new LiteralControl("<br />"));
        }
        /// <summary>
        /// Adding server names to drop down list
        /// </summary>
        /// <param name="servers"></param>
        /// <param name="dropDownList"></param>
        public static void AddServerNamesToDropDownList(List<Server> servers, DropDownList dropDownList)
        {
            servers.ForEach(server =>
            {
                dropDownList.Items.Add(new ListItem(server.Name));
            });
        }
        /// <summary>
        /// Forming TreeView from selected server
        /// </summary>
        /// <param name="name">Selected servers name</param>
        /// <param name="registers">All data registers</param>
        /// <param name="treeView">Tree</param>
        /// <returns>List for writing to txt file</returns>
        public static List<Day> FormTreeView(string name, List<DayRegister> registers, TreeView treeView)
        {
            List<Day> days = (from register in registers
                              from reg in register
                              where reg.Name == name
                              select reg).ToList();



            TreeNode node = new TreeNode();

            days.ForEach(d =>
            {
                node = new TreeNode();
                string[] values = d.SplitedPath;

                if (values.Length > 1)
                {
                    TreeNode treenode = treeView.Nodes.Cast<TreeNode>().Where(n => n.Text == values[1]).FirstOrDefault();
                    if (treenode == null)
                    {
                        node.Text = values[1];
                        GetChilds(values, node, 2);
                    }
                    else
                    {
                        GetChilds(values, treenode, 2);
                    }
                }

                if (node.Text != "") treeView.Nodes.Add(node);
            });

            return days;
        }
        /// <summary>
        /// Creating and putting children in TreeNode (recursive)
        /// </summary>
        /// <param name="d">Full path splited</param>
        /// <param name="node">Node to put</param>
        /// <param name="index">Index of current path value to work with</param>
        private static void GetChilds(string[] d, TreeNode node, int index)
        {
            if (index == d.Length) return;

            TreeNode treenode = node.ChildNodes.Cast<TreeNode>().Where(n => n.Text == d[index]).FirstOrDefault();
            if (treenode == null)
            {
                TreeNode child = new TreeNode();
                child.Text = d[index];
                node.ChildNodes.Add(child);

                GetChilds(d, child, index + 1);
            }
            else
            {
                GetChilds(d, treenode, index + 1);
            }
        }
        /// <summary>
        /// Sorting all main nodes of TreeView and calling to sort children
        /// </summary>
        /// <param name="treeView"></param>
        public static void SortTreeView(TreeView treeView)
        {
            foreach (TreeNode item in treeView.Nodes)
            {
                NodeSorter(item);
            }

            List<TreeNode> node1 = (from n in treeView.Nodes.Cast<TreeNode>().ToList()
                                    orderby n.Text
                                    select n).ToList();
            treeView.Nodes.Clear();

            node1.ForEach(n => treeView.Nodes.Add(n));
        }
        /// <summary>
        /// Recursively sorting using linq all node children
        /// </summary>
        /// <param name="treeNode">Tree node</param>
        private static void NodeSorter(TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.ChildNodes)
            {
                NodeSorter(node);
            }

            List<TreeNode> node1 = (from n in treeNode.ChildNodes.Cast<TreeNode>().ToList()
                                    orderby n.Text
                                    select n).ToList();
            treeNode.ChildNodes.Clear();

            node1.ForEach(n => treeNode.ChildNodes.Add(n));
        }

        
    }
}