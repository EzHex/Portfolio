package main

import (
	"bufio"
	"fmt"
	"log"
	"math/rand"
	"os"
	"sort"
	"strconv"
	"strings"
)

type BusinessManager struct {
	name          string
	workYears     int
	averageProfit float64
	profit        float64
}

func check(err error) {
	if err != nil {
		log.Fatalf("error: %s", err)
	}
}

func readLines(path string) ([]string, error) {
	file, err := os.Open(path)
	if err != nil {
		return nil, err
	}
	defer file.Close()

	var lines []string
	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}
	return lines, scanner.Err()
}

func writeLines(lines []string, path string) error {
	file, err := os.Create(path)
	if err != nil {
		return err
	}
	defer file.Close()

	w := bufio.NewWriter(file)
	for _, line := range lines {
		fmt.Fprintln(w, line)
	}
	return w.Flush()
}

func convertToString(managers []BusinessManager) []string {
	var result []string

	for _, manager := range managers {
		line := fmt.Sprintf("| %-10s | %5d | %10.2f | %10.2f |", manager.name, manager.workYears, manager.averageProfit, manager.profit)
		result = append(result, line)
	}

	return result
}

func fRand(fMin float64, fMax float64) float64 {
	return fMin + (fMax-fMin)*rand.Float64()
}

func calculateProfit(averageProfit float64, workYears int) float64 {
	var result float64 = 0

	for i := 0; i < workYears; i++ {
		result += (averageProfit + fRand(0, 100))
	}

	return result
}

func worker(input <-chan BusinessManager, output chan<- BusinessManager, exit chan bool) {
	for {
		manager, up := <-input
		if !up {
			break
		} else {
			manager.profit = calculateProfit(manager.averageProfit, manager.workYears)
			if manager.profit > 500 {
				output <- manager
			}
		}
	}
	select {
	case exit <- true:
		//uzdaromos gijos
	default:
		//paskutine gija uzdaro kanala
		close(output)
	}
}

func dataProcess(input <-chan BusinessManager, output chan<- BusinessManager) {
	tempManagers := make([]BusinessManager, 12)
	count := 0
	done := false
	for !done {
		if count > 0 {
			select {
			case output <- tempManagers[count-1]:
				count -= 1
			default:

			}
		}

		if count < 12 {
			select {
			case manager, up := <-input:
				if !up {
					if count <= 0 {
						done = true
					}
				} else {
					tempManagers[count] = manager
					count += 1
				}
			default:

			}
		}
	}
	close(output)
}

func resultProcess(input <-chan BusinessManager, output chan<- BusinessManager) {
	tempManagers := make([]BusinessManager, 25)
	count := 0
	done := false
	for !done {

		if count > 0 {
			select {
			case output <- tempManagers[count-1]:
				count -= 1
			default:

			}
		}

		if count < 25 {
			select {
			case manager, up := <-input:
				if !up {
					if count <= 0 {
						done = true
					}
				} else {
					tempManagers[count] = manager
					count += 1
				}
			default:

			}
		}
	}
	close(output)
}

func main() {
	fmt.Println("Starting program")

	var businessManagers []BusinessManager
	var resultBusinessManagers []BusinessManager

	lines, err := readLines("input_files/dat_2.txt")
	check(err)

	for _, line := range lines {
		variables := strings.Split(line, " ")
		name := string(variables[0])
		workY, err1 := strconv.Atoi(variables[1])
		check(err1)
		avgProfit, err2 := strconv.ParseFloat(variables[2], 32)
		check(err2)
		manager := BusinessManager{name: name, workYears: workY, averageProfit: avgProfit}
		businessManagers = append(businessManagers, manager)
	}

	var N int = len(businessManagers) / 4

	var dataInput = make(chan BusinessManager)
	var dataOutput = make(chan BusinessManager)
	var resultInput = make(chan BusinessManager)
	var resultOutput = make(chan BusinessManager)
	//paskutine gija uzdaro kanala
	//n - 1, kad zinot jog visos kitos irgi pasiruosiusios baigti darba
	var exit = make(chan bool, N-1)

	for i := 0; i < N; i++ {
		go worker(dataOutput, resultInput, exit)
	}

	go dataProcess(dataInput, dataOutput)
	go resultProcess(resultInput, resultOutput)

	for _, manager := range businessManagers {
		dataInput <- manager
	}
	close(dataInput)

	for {
		manager, up := <-resultOutput
		if !up {
			break
		} else {
			resultBusinessManagers = append(resultBusinessManagers, manager)
		}
	}

	sort.Slice(resultBusinessManagers, func(i, j int) bool {
		return resultBusinessManagers[i].profit > resultBusinessManagers[j].profit
	})
	err3 := writeLines(convertToString(resultBusinessManagers), "result.txt")
	check(err3)

	fmt.Println("Done")
}
