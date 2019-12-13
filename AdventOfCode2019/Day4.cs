using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    // https://adventofcode.com/2019/day/4
    public static class Day4
    {
        // Part 1
        public static bool IsPossiblePassword(int password)
        {
            if (password < 0 || password >= 1000000)
                return false;

            string s = password.ToString("D6");

            // Two adjacent digits are the same.
            if (!Regex.IsMatch(s, @"(.)\1"))
                return false;

            // Going from left to right, the digits never decrease.
            int prev = 0;
            foreach (int n in s.Select(c => c - '0'))
            {
                if (n < prev)
                    return false;
                prev = n;
            }

            return true;
        }

        // Part 2
        public static bool IsPossiblePasswordStrict(int password)
        {
            if (password < 0 || password >= 1000000)
                return false;

            string s = password.ToString("D6");

            // Two adjacent digits are the same and are not part of a larger group of matching digits.
            if (!Regex.IsMatch(s, @"(^|(?<a>.)(?!\k<a>))(?<b>.)\k<b>(?!\k<b>)"))
                return false;

            // Going from left to right, the digits never decrease.
            int prev = 0;
            foreach (int n in s.Select(c => c - '0'))
            {
                if (n < prev)
                    return false;
                prev = n;
            }

            return true;
        }
    }
}
