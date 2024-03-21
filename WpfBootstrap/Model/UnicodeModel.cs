using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBootstrap.Model
{
    public class UnicodeModel
    {
        public string Icon
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Code))
                {
                    var newcode = Code.Replace("&#x", "").TrimEnd(';');
                    int codePoint = int.Parse(newcode, System.Globalization.NumberStyles.HexNumber);
                    string _icon = char.ConvertFromUtf32(codePoint);
                    return _icon;
                }
                return Code;
            }
        }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
