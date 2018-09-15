using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchool.Models
{
    public class SimpleModel
    {
        public int numId { get; set; }
        public String strId { get; set; }
        public String name { get; set; }

        public SimpleModel(int numId, String name)
        {
            this.numId = numId;
            this.name = name;
        }

        public SimpleModel(String strId, String name)
        {
            this.strId = strId;
            this.name = name;
        }

    }
}