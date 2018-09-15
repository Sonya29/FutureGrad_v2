using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smart.Models
{
	public class Coordinates
	{
		public static String generateCode(List<List<String>> codes)
		{
			String codeAsString = "";
			foreach(var code in codes){
				codeAsString = codeAsString + code[0] + "," + code[1] + "," + code[2] + "||";
			}
			return codeAsString;
		}

	}
}
