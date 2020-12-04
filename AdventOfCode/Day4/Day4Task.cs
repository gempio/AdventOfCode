using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day4
{
    public class Day4Task
    {
        public string Execute()
        {
            var input = File.ReadAllLines("./Day4/Day4Input.txt");
            bool done = false;
            var yPosition = 0;
            int goodPassports = 0;
            var passport = new Passport();

            while (!done)
            {
                if (yPosition >= input.Length) break;
                if (input[yPosition].Trim() is "")
                {
                    if (passport.IsValid())
                    {
                        goodPassports++;
                    }

                    passport = new Passport();
                }
                else
                {
                    var passportInformation = input[yPosition].Split(' ');
                    foreach (var passInfo in passportInformation)
                    {
                        var keyValueInfo = passInfo.Split(':');
                        int placeholderInt;
                        switch (keyValueInfo[0])
                        {
                            case "byr":
                                if (int.TryParse(keyValueInfo[1], out placeholderInt) && placeholderInt > 1919 && placeholderInt < 2003)
                                {
                                    passport.Byr = keyValueInfo[1];
                                }
                               
                                break;
                            case "iyr":
                                if (int.TryParse(keyValueInfo[1], out placeholderInt) && placeholderInt > 2009 && placeholderInt < 2021)
                                {
                                    passport.Iyr = keyValueInfo[1];
                                }
                                else
                                {
                                    Console.WriteLine($"invalid iyr:{keyValueInfo[1]}");
                                }
                                break;
                            case "eyr":
                                if (int.TryParse(keyValueInfo[1], out placeholderInt) && placeholderInt > 2019 && placeholderInt < 2031)
                                {
                                    passport.Eyr = keyValueInfo[1];
                                }
                                break;
                            case "hgt":
                                if (keyValueInfo[1].Contains("cm"))
                                {
                                    if (int.TryParse(keyValueInfo[1].Replace("cm",""), out placeholderInt) && placeholderInt >= 150 && placeholderInt <= 193)
                                    {
                                        passport.Hgt = keyValueInfo[1];
                                    }
                                }
                                if (keyValueInfo[1].Contains("in"))
                                {
                                    if (int.TryParse(keyValueInfo[1].Replace("in",""), out placeholderInt) && placeholderInt >= 59 && placeholderInt <= 76)
                                    {
                                        passport.Hgt = keyValueInfo[1];
                                    }
                                }
                                break;
                            case "hcl":
                                Regex regex = new Regex("[#][0-9a-f]{6}");

                                if (regex.Matches(keyValueInfo[1]).Count > 0)
                                {
                                    passport.Hcl = keyValueInfo[1];
                                }
                                break;
                            case "ecl":
                                if(keyValueInfo[1] == "amb" || keyValueInfo[1] == "blue" || keyValueInfo[1] == "brn" || keyValueInfo[1] == "gry" || keyValueInfo[1] == "grn" || keyValueInfo[1] == "hzl" || keyValueInfo[1] == "oth") passport.Ecl = keyValueInfo[1];
                                break;
                            case "pid":
                                Regex regex2 = new Regex("[0-9]{9}");

                                if (regex2.Matches(keyValueInfo[1]).Count > 0)
                                {
                                    passport.Hcl = keyValueInfo[1];
                                }
                                passport.Pid = keyValueInfo[1];
                                break;
                            case "cid":
                                passport.Cid = keyValueInfo[1];
                                break;
                        }
                    }
                }


                yPosition++;
            }
            return goodPassports.ToString();
        }
    }

    public class Passport
    {
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Byr) && !string.IsNullOrEmpty(Iyr) && !string.IsNullOrEmpty(Eyr) &&
                   !string.IsNullOrEmpty(Hgt) && !string.IsNullOrEmpty(Hcl) && !string.IsNullOrEmpty(Ecl) &&
                   !string.IsNullOrEmpty(Pid); //&& !string.IsNullOrEmpty(Iyr)
        }

        public string Byr { get; set; }
        public string Iyr { get; set; }
        public string Eyr { get; set; }
        public string Hgt { get; set; }
        public string Hcl { get; set; }
        public string Ecl { get; set; }
        public string Pid { get; set; }
        public string Cid { get; set; }
    }
}
