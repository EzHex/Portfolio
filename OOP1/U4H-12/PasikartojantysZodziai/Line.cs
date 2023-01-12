using System.Linq;

namespace PasikartojantysZodziai
{
    class Line
    {
        public string Text { get; set; }
        public string[] SplitText { get; set; }
        public int Count { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// Lengvam teksto redagavimui, susiformatuoju eilutes: susidedu žodžius su skyrikliais, suskaičiuojų kiek yra eilutėje žodžių.
        /// </summary>
        /// <param name="text">Tekstas iš duomenų failo</param>
        public Line(string text)
        {
            this.Text = text;
            this.SplitText = this.Text.Split(' ');
            this.Count = SplitText.Count();
            this.Index = 0;
        }

        public Line()
        {

        }
    }
}
