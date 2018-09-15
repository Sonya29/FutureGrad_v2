using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Smart.Models
{
    public class Language
    {
        //private List<String> terminology = getAlternativeText();

        public static String replace_terminology(String source, String toLanguage, String fromLanguage = "English")
        {
            String result = "";
            string line;
            int toLanguagePos = getLanguagePos(toLanguage, @"C:\Users\Sonya.SHAJI-DOMAIN\Documents\Smart\Smart\Models\Language.cs");
            if(toLanguagePos > -1)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\Sonya.SHAJI-DOMAIN\Documents\Smart\Smart\Models\Language.cs");
                while ((line = file.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    String[] alt = line.Split('|');
                }
                file.Close();
            }else
            {
                result = source;
            }           
            return result;
        }
        private static int getLanguagePos(String language, String filepath)
        {
            int result = -1;
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            String line = file.ReadLine();
            String[] alt = line.Split('|');
            for(int i = 0; i < alt.Length; i++)
            {
                if (alt[i] == language)
                {
                    result = i;
                    i = alt.Length + 1;
                }
            }
            file.Close();
            return result;
        }
    }
}