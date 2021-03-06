﻿/*
 * Idmr.ProjectHex.Compute.dll, Calculation library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.1.5+
 */

/* CHANGELOG
 * [UPD] removed "~" from isValid(), since it's removed in formatTnput() anyway
 * [FIX] exponents were really being treated as bitwise NOT, added # (POUnd, POWer...) operator for it
 * [ADD] hex values with "0x" or "0X" prefix supported
 * v0.1.5, 150705
 * [ADD] support for "<>", "~=" notation for "not equal"
 * [ADD] support for "**" notation for exponents
 * v0.1.4, 130910
 * [UPD] License
 * v0.1.1, 130421
 */

using System;

namespace Idmr.ProjectHex
{
	/// <summary>Object to evaluate boolean statements.</summary>
	public static class Conditional
	{
		/// <summary>Determines if the input is formatted properly.</summary>
		/// <param name="cond">Conditional to be evaluated.</param>
		/// <exception cref="ArgumentNullException"><paramref name="cond"/> is <b>null</b> or empty.</exception>
		/// <returns><b>false</b> if illegal characters are detected, otherwise <b>true</b>.</returns>
		public static bool IsValid(string cond)
		{
			try { return isValid(ref cond); }
			catch (ArgumentNullException) { throw; }
		}

		/// <exception cref="ArgumentNullException"><paramref name="cond"/> is <b>null</b> or empty.</exception>
		static bool isValid(ref string cond)
		{
			try { formatInput(ref cond); }
			catch (ArgumentNullException) { throw; }
			string allowed = "0123456789+-*/%#<>&^|().,=!TFX";
			for (int i = 0; i < cond.Length; i++)
				if (allowed.IndexOf(cond[i]) == -1) return false;
			// reject isolated '='
			if (cond.IndexOf("=") != -1 && cond.IndexOf("<=") == -1 && cond.IndexOf(">=") == -1 && cond.IndexOf("!=") == -1 && cond.IndexOf("==") == -1) return false;
			// ensure proper number format definition (will be converted back to Lower within Equation)
			if (cond.IndexOf("X") != -1 && cond.IndexOf("0X") == -1) return false;
			return true;
		}

		/// <summary>Computes a given conditional and returns the result as a boolean.</summary>
		/// <remarks>Operation support: "+ - * / % # ^^ ** () {} [] &lt;&lt; &gt;&gt; & ^ |", decimals.<br/>
		/// Condition support: "&lt; &gt; &lt;= &gt;= == (!= ~= <> NE) && ||"<br/>
		/// Bit-wise operations (&lt;&lt; &gt;&gt; & ^ |) do not support decimals. Using these operations will round as necessary during calculation.<br/>
		/// Boolean values may be included in <paramref name="cond"/> as <b>true/T/yes</b> and <b>false/F/no</b> (case-insensitive).<br/>
		/// Bitshift operators can be used in conjunction with &lt; and &gt; conditionals with or without parenthesis, however the use of parenthesis provides better performance and clarity (ie, "(x>>y)>z" is preferred over "x>>y>z" although both yield the same result).<br/>
		/// If <paramref name="cond"/> evaluates to a numerical value instead of <b>true</b>/<b>false</b>, non-zero results will return <b>true</b>.<br/>
		/// For exponents, the "<b>#</b>" operator is used due to "<b>^</b>" being used for bitwise NOT. "<b>^^</b>" and "<b>**</b>" are also acceptable notations. Care must be taken for nested exponents, as "x # y # z" will evaluate to (x#y)#z.<br/>
		/// For "not equal", "<b>!=</b>", "<b>~=</b>", "<b>&lt;&gt;</b>" and "<b>NE</b>" (case-insensitive) are acceptable notations.<br/>
		/// Hex values are supported, must use "<b>0x</b>" (case-insensitive) prefix.</remarks>
		/// <param name="cond">Conditional to be evaluated</param>
		/// <exception cref="ArgumentException"><paramref name="cond"/> contains logical errors.</exception>
		/// <exception cref="FormatException">Illegal characters present.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="cond"/> is <b>null</b> or empty.</exception>
		/// <returns>Calculated result.</returns>
		public static bool Evaluate(string cond)
		{
			try { if (!isValid(ref cond)) throw new FormatException("Error: Conditional contains illegal characters"); }
			catch (ArgumentNullException) { throw; }
			if (cond[0] == '-') cond = "0" + cond;
			string result = "";
			try { result = calculate(cond); }
			catch (ArgumentException x) { throw new ArgumentException("Error: " + x.Message); }
			return (result != "F" && result != "0");
		}

		/// <exception cref="ArgumentNullException"><paramref name="cond"/> is <b>null</b> or empty.</exception>
		static void formatInput(ref string cond)
		{
			if (cond == "" || cond == null) throw new ArgumentNullException("Error: Conditional is empty");
			cond = cond.Replace(" ", "");
			cond = cond.Replace('{', '(');
			cond = cond.Replace('[', '(');
			cond = cond.Replace('}', ')');
			cond = cond.Replace(']', ')');
			cond = cond.Replace("**", "#");
			cond = cond.Replace("^^", "#");
			cond = cond.Replace("~=", "!=");
			cond = cond.Replace("<>", "!=");
			cond = cond.ToUpper();
			cond = cond.Replace("NE", "!=");
			cond = cond.Replace("TRUE", "T");
			cond = cond.Replace("FALSE", "F");
			cond = cond.Replace("YES", "T");
			cond = cond.Replace("NO", "F");
		}
		
		/// <exception cref="ArgumentException">Bracket mismatch<br/><b>-or-</b><br/>Empty term</exception>
		static string calculate(string cond)
		{
			if (cond == "") throw new ArgumentException("Empty term found");
			if (cond.IndexOf("(") != -1)
			{
				int leftIndex = cond.LastIndexOf("(");
				int rightIndex = cond.IndexOf(")", leftIndex);
				if (rightIndex == -1) throw new ArgumentException("Bracket mismatch, closing bracket not found");
				return calculate(cond.Substring(0, leftIndex) + calculate(cond.Substring(leftIndex + 1, rightIndex - leftIndex - 1)) + cond.Substring(rightIndex + 1));
			}
			else if (cond.IndexOf(")") != -1) throw new ArgumentException("Bracket mismatch, opening bracket not found");
			string left, right;
			if (cond.IndexOf("&&") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("&&")));
				right = calculate(cond.Substring(cond.IndexOf("&&") + 2));
				return ((left == "T") && (right == "T") ? "T" : "F");
			}
			else if (cond.IndexOf("||") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("||")));
				right = calculate(cond.Substring(cond.IndexOf("||") + 2));
				return ((left == "T") || (right == "T") ? "T" : "F");
			}
			else if (cond.IndexOf("==") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("==")));
				right = calculate(cond.Substring(cond.IndexOf("==") + 2));
				return (left == right ? "T" : "F");
			}
			else if (cond.IndexOf("!=") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("!=")));
				right = calculate(cond.Substring(cond.IndexOf("!=") + 2));
				return (left != right ? "T" : "F");
			}
			else if (cond.IndexOf(">=") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf(">=")));
				right = calculate(cond.Substring(cond.IndexOf(">=") + 2));
				return (double.Parse(left) >= double.Parse(right) ? "T" : "F");
			}
			else if (cond.IndexOf("<=") != -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("<=")));
				right = calculate(cond.Substring(cond.IndexOf("<=") + 2));
				return (double.Parse(left) <= double.Parse(right) ? "T" : "F");
			}
			else if (cond.IndexOf("<") != -1 && cond.IndexOf("<<") == -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf("<")));
				right = calculate(cond.Substring(cond.IndexOf("<") + 1));
				return (double.Parse(left) < double.Parse(right) ? "T" : "F");
			}
			else if (cond.IndexOf(">") != -1 && cond.IndexOf(">>") == -1)
			{
				left = calculate(cond.Substring(0, cond.IndexOf(">")));
				right = calculate(cond.Substring(cond.IndexOf(">") + 1));
				return (double.Parse(left) > double.Parse(right) ? "T" : "F");
			}
			if (cond.IndexOf("<<") != -1)
			{
				string section = cond;
				int offset = 0;
				for (; offset < cond.Length; )
				{
					int shiftIndex = section.IndexOf("<<");
					int compareIndex = section.IndexOf("<");
					if (compareIndex == -1)
					{
						// '<' does not exist by itself
						break;
					}
					else if (compareIndex == shiftIndex)
					{
						// skip over "<<"
						offset += shiftIndex + 2;
						section = cond.Substring(offset);
						continue;
					}
					else
					{
						// found a stand-alone '<'
						offset += compareIndex;
						left = calculate(cond.Substring(0, offset));
						right = calculate(cond.Substring(offset + 1));
						return (double.Parse(left) < double.Parse(right) ? "T" : "F");
					}
				}
			}
			if (cond.IndexOf(">>") != -1)
			{
				string section = cond;
				for (int offset = 0; offset < cond.Length; )
				{
					int shiftIndex = section.IndexOf(">>");
					int compareIndex = section.IndexOf(">");
					if (compareIndex == -1)
					{
						// '>' does not exist by itself
						break;
					}
					else if (compareIndex == shiftIndex)
					{
						// skip over ">>"
						offset += shiftIndex + 2;
						section = cond.Substring(offset);
						continue;
					}
					else
					{
						// found a stand-alone '>'
						offset += compareIndex;
						left = calculate(cond.Substring(0, offset));
						right = calculate(cond.Substring(offset + 1));
						return (double.Parse(left) > double.Parse(right) ? "T" : "F");
					}
				}
			}
			if (cond == "T" || cond == "F") return cond;
			else return Equation.Evaluate(cond);
		}
	} 
}