using System;
using System.Collections.Generic;

namespace simple_select_statement
{
    public class SssTranspiler
    {
        private string _simpleSelect;
        private List<string> _simpleOrders = new List<string>();
        private List<string> _simplewheres = new List<string>();

        public SssTranspiler(string sss)
        {

            string[] parts = sss.Split("|");
            int index = 0;
            while (index < parts.Length)
            {
                if (index == 0)
                {
                    _simpleSelect = parts[index];
                }
                else if (parts[index] == string.Empty)
                {
                    //order
                    do
                    {
                        index++;
                    } while (parts[index] == string.Empty);
                    _simpleOrders.Add(parts[index]);
                }
                else
                {
                    // where
                    _simplewheres.Add(parts[index]);
                }

                index++;
            };

            Console.WriteLine("order:" + string.Join(",", _simpleOrders));
            //string[] where = sss.Split("|");
            Console.WriteLine("where:" + string.Join(",", _simplewheres));

        }


        public string TranspileToSql()
        {
            

            return $"{processSelect()} {processWhere()} {ProcessOrder()}";
        }

        private string processSelect()
        {
            //format: table1.[*] or table1.field1
            string[] parts = _simpleSelect.Split(".");
            if (parts.Length < 2)
            {
                throw new InvalidCastException("invalid statement");
            }
            string table = parts[0];
            string fields = parts[1];
            if (fields.Contains("[") || fields.Contains("]"))
            {
                fields = fields.Replace("[", "").Replace("]", "");
            }

            return $"SELECT {fields} FROM {table}";
        }

        private string processWhere()
        {
            //format: field1.eq().and().field2.ne();
            string[] parts = string.Join(".and().", _simplewheres.ToArray()).Split(".");
            int index = 0;
            string result = "";
            while (index < parts.Length)
            {
                switch (parts[index])
                {
                    case string part when part.Contains("eq("):
                        result += $" ={getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("ne("):
                        result += $" !={getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("gt("):
                        result += $" >{getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("gte("):
                        result += $" >={getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("lt("):
                        result += $" <{getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("lte("):
                        result += $" <={getStringBetween(parts[index], "(", ")")}";
                        break;
                    case string part when part.Contains("in("):
                        result += $" IN ({getStringBetween(parts[index], "(", ")")})";
                        break;
                    case string part when part.Contains("nin("):
                        result += $" NOT IN ({getStringBetween(parts[index], "(", ")")})";
                        break;
                    case string part when part.Contains("and"):
                        result += " AND ";
                        break;
                    case string part when part.Contains("or"):
                        result += " OR ";
                        break;
                    default:
                        result +=" " + parts[index];
                        break;

                }
                index++;
            }

            return "WHERE " + result;
        }

        private string ProcessOrder()
        {
            return $"ORDER BY {string.Join(",",_simpleOrders)}";
        }


        private string getStringBetween(string keyIn, string left, string right)
        {
            int form = keyIn.IndexOf(left);
            int to = keyIn.IndexOf(right);
            return keyIn.Substring(form+1, to - form-1);
        }

        private string processAlias(string statement)
        {
            return statement.Replace("@"," AS ")
        } 

    }
}
