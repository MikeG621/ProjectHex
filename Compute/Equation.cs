/*
 * Idmr.ProjectHex.Compute.dll, Calculation library file
 * Copyright (C) 2012- Michael Gaisser (mjgaisser@gmail.com)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
 * with this file, You can obtain one at http://mozilla.org/MPL/2.0/
 *
 * Version: 0.0.4
 */
 
/* CHANGELOG
 * v0.0.4, 130910
 * [UPD] License
 * v0.0.1, 130421
 */
 
using System;

namespace Idmr.ProjectHex
{
	/// <summary>Object to evaluate numerical equations.</summary>
	public static class Equation
	{
		/// <summary>Determines if the input is formatted properly.</summary>
		/// <param name="eq">Equation to be evaluated.</param>
		/// <exception cref="ArgumentNullException"><i>eq</i> is <b>null</b> or empty.</exception>
		/// <returns><b>false</b> is illegal characters are detected, otherwise <b>true</b>.</returns>
		public static bool IsValid(string eq)
		{
			try { return isValid(ref eq); }
			catch (ArgumentNullException) { throw; }
		}

		/// <exception cref="ArgumentNullException"><i>eq</i> is <b>null</b> or empty.</exception>
		static bool isValid(ref string eq)
		{
			try { formatInput(ref eq); }
			catch (ArgumentNullException) { throw; }
			string allowed = "0123456789+-*/%<>&^|().,";
			for (int i = 0; i < eq.Length; i++)
				if (allowed.IndexOf(eq[i]) == -1) return false;
			// reject isloated '<' and '>'
			if ((eq.IndexOf("<") != -1 && eq.IndexOf("<<") == -1) || (eq.IndexOf(">") != -1 && eq.IndexOf(">>") == -1)) return false;
			return true;
		}

		/// <summary>Computes a given equation and returns the result as a string.</summary>
		/// <remarks>Supports: "+ - * / % () {} [] &lt;&lt; &gt;&gt; & ^ |", decimals.<br/>
		/// Bit-wise operations (&lt;&lt; &gt;&gt; & ^ |) do not support decimals. Using these operations will round as necessary during calculation.</remarks>
		/// <param name="eq">Equation to be evaluated</param>
		/// <exception cref="ArgumentException"><i>eq</i> contains logical errors.</exception>
		/// <exception cref="FormatException">Illegal characters present.</exception>
		/// <exception cref="ArgumentNullException"><i>eq</i> is <b>null</b> or empty.</exception>
		/// <returns>Calculated result.</returns>
		public static string Evaluate(string eq)
		{
			try { if (!isValid(ref eq)) throw new FormatException("Error: Equation contains illegal characters"); }
			catch (ArgumentNullException) { throw; }
			if (eq[0] == '-') eq = "0" + eq;
			string result = "";
			try { result = calculate(eq).ToString(); }
			catch (ArgumentException x) { throw new ArgumentException("Error: " + x.Message); }
			return result;
		}
		
		/// <exception cref="NullReferenceException"><i>eq</i> is <b>null</b> or empty.</exception>
		static void formatInput(ref string eq)
		{
			if (eq == "" || eq == null) throw new ArgumentException("Error: Equation is empty");
			eq = eq.Replace(" ", "");
			eq = eq.Replace('{', '(');
			eq = eq.Replace('[', '(');
			eq = eq.Replace('}', ')');
			eq = eq.Replace(']', ')');
		}
		
		/// <exception cref="ArgumentException">Bracket mismatch<br/><b>-or-</b><br/>Empty term</exception>
		static double calculate(string eq)
		{
			// OoO: (), * / %, + -, << >>, &, ^, |
			if (eq == "") throw new ArgumentException("Empty term found");
			if (eq.IndexOf("(") != -1)
			{
				int left = eq.LastIndexOf("(");
				int right = eq.IndexOf(")", left);
				if (right == -1) throw new ArgumentException("Bracket mismatch, closing bracket not found");
				double middle = calculate(eq.Substring(left + 1, right - left - 1));
				return calculate(eq.Substring(0, left) + middle.ToString() + eq.Substring(right + 1));
			}
			else if (eq.IndexOf(")") != -1) throw new ArgumentException("Bracket mismatch, opening bracket not found");
			double result = 0;
			if (eq.IndexOf('|') != -1)
			{
				string[] bitOr = eq.Split('|');
				long integerResult = (long)calculate(bitOr[0]);
				for (int i = 1; i < bitOr.Length; i++) integerResult |= (long)calculate(bitOr[i]);
				return (double)integerResult;
			}
			else if (eq.IndexOf('^') != -1)
			{
				string[] bitNot = eq.Split('^');
				long integerResult = (long)calculate(bitNot[0]);
				for (int i = 1; i < bitNot.Length; i++) integerResult ^= (long)calculate(bitNot[i]);
				return (double)integerResult;
			}
			else if (eq.IndexOf('&') != -1)
			{
				string[] bitAnd = eq.Split('&');
				long integerResult = (long)calculate(bitAnd[0]);
				for (int i = 1; i < bitAnd.Length; i++) integerResult &= (long)calculate(bitAnd[i]);
				return (double)integerResult;
			}
			else if (eq.IndexOf("<<") != -1)
			{
				string[] bitLeft = eq.Split('<');
				long integerResult = (long)calculate(bitLeft[0]);
				for (int i = 2; i < bitLeft.Length; i += 2) integerResult <<= (int)calculate(bitLeft[i]);
				return (double)integerResult;
			}
			else if (eq.IndexOf(">>") != -1)
			{
				string[] bitRight = eq.Split('>');
				long integerResult = (long)calculate(bitRight[0]);
				for (int i = 2; i < bitRight.Length; i += 2) integerResult >>= (int)calculate(bitRight[i]);
				return (double)integerResult;
			}
			else if (eq.IndexOf('+') != -1)
			{
				string[] plus = eq.Split('+');
				for (int i = 0; i < plus.Length; i++) result += calculate(plus[i]);
				return result;
            }
			else if (eq.IndexOf('-') != -1)
			{
				string[] minus = eq.Split('-');
				result = calculate(minus[0]);
				for (int i = 1; i < minus.Length; i++) result -= calculate(minus[i]);
				return result;
			}
			else if (eq.IndexOf('*') != -1)
			{
				string[] mult = eq.Split('*');
				result = calculate(mult[0]);
				for (int i = 1; i < mult.Length; i++) result *= calculate(mult[i]);
				return result;
			}
			else if (eq.IndexOf('/') != -1)
			{
				string[] div = eq.Split('/');
				result = calculate(div[0]);
				for (int i = 1; i < div.Length; i++) result /= calculate(div[i]);
				return result;
			}
			else if (eq.IndexOf('%') != -1)
			{
				string[] mod = eq.Split('%');
				result = calculate(mod[0]);
				for (int i = 1; i < mod.Length; i++) result %= calculate(mod[i]);
				return result;
			}
			else return double.Parse(eq);
		}
	} 
}
