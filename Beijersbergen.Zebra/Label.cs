using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beijersbergen.Zebra
{
    public class Label
    {
        public string Name { get; set; }
        public string ArticleCode { get; set; }
        public string BatchNumber { get; set; }
        public DateTime ProductionDate { get; set; }
        public string Weight { get; set; }
        public string QrCodeContent { get; set; }
        public bool ShowCorrosiveIndicator { get; set; }
        public int MagnificationFactor { get; set; }

        public string GenerateZPL(string templatePath)
        {
            var zpl = File.ReadAllText(templatePath);

            zpl = zpl.Replace("<<Name>>", Name);
            zpl = zpl.Replace("<<BatchNumber>>", BatchNumber);
            zpl = zpl.Replace("<<ProductionDate>>", ProductionDate.ToString("dd-MM-yyyy"));
            zpl = zpl.Replace("<<Weight>>", Weight);
            zpl = zpl.Replace("<<QrCode>>", QrCodeContent);
            zpl = zpl.Replace("<<MagnificationFactor>>", MagnificationFactor.ToString());
            //zpl = zpl.Replace("<<128Code>>", $"AMA050FUSPT10000005-B{OrderCode}");
            zpl = zpl.Replace("<<128Code>>", $"{ArticleCode}-B{BatchNumber}");

            // TODO: Corrosive indicator
            zpl = ShowCorrosiveIndicator
                ? zpl.Replace("<<CorrosiveIndicator>>", corrosiveIndicatorZpl)
                : zpl.Replace("<<CorrosiveIndicator>>", string.Empty);

            return zpl;
        }


        private static string corrosiveIndicatorZpl = @"^FO32,192^GFA,32768,32768,00064,:Z64:
eJztmjFu5DYUhknLswyQwGocbGNYexBj5CP4BnOFlFPIlnKS5CaRsQjSbrF9aLhxlchIAitYzbx9pDRjGVgE4P8SDIy8v7Cm8IdPlMgnkpIxGo1Go9FoNBqNRqPRaDQajUaj0Wg0Go1Go9FoNJov5zj+tVfGnEL8m/HQG3MG8W70M38O8Xm782O8i7wdjCkwf7/zY7yjj2ZsP8ZntBX5LZHIb4ia0Z83EL+ilv0d+1vsBMqJz0G+2Iy8A3nHF0Dit9SJ/Ib5zDPvIbo2qyG6QZ6abOQziLf0wU58h/l7cxP7HsZz/788NbkZ+wDA9/Qwjh2M5xvAA0DCl3z/l+FHj/GO+RsBP94BAf+2N1U4ViBv3pkhHNYoH8u/wM+doMmadH946o4/Ng2PgHT/djpmfeAvkvn6Yxj/ZnW7aSF/ueXnT2PJDy3kL4jzuKLuWsBzfN1yBVjCfMc80v4im/jSF5CfBz9top8rQHr788A/lORLXxrM/0Sto7b2Neanjq1lU/ot6B+LXvnXgPAnhvzIcw2C/BNfcw2U+T3kt2HyE/2XCH9uIv+VqUMlgPmzeBuOYX5pPjH/TTp/Ftu/IPPI/Fk6vzRh8smND/xFOn8R/I4GG/jqJp0P/oL6kU/3V5bMaUEfssinj7/KkCl++s28v2N+wPwFtW9/uesur6r0+reOfEPs92vAP/KObu8euzXg7+3W5NucbOCH9PvXsX9xtYp8Bfg7G8ZdOdh79vfJvA1+7r9X9g/QnwX/1tj7+64aEP+WR99g7BPktz5cfy69lu56rP0D339e/hDkzzy3P5Te4K+q5Pknn/9g6l/byT8A/mGc+n7i9qf7s5b52GouINU1xsdZZ3bXretk3gXe7Px18vqD/ZOTC9g6nXd73mJ888LfpPvds7+qU3Fede7P/+9uvf3nf/6S37huz18nd7/g310zgvhnP09BEX7mT+9+PP1yMz59+ZsL+Zm/RvjzZz7nKbzE77r07hf8fvppu/TbNz9/iF/O/D2w+r+Y+Tcyf9iESE4154Hdl4sZD3SfF36Mz/c8svta4XuOMeuZH0kF75nu/SL+0P41vOe99zcSvhL7C5H/0HyFvvPY+0sRXwn5JbDkn+fGpC+Z5ikNULRnKW6FPFL0Z8mRoj+Lm3Z/0Ox2f+CkTzlf5ljIv/Z8eynjay/CrbD/ZeL+Kxt/uReO3xZ/5RL9jaz+FcL6X8T3hgflGxkvm/+VvAI/sF/ES/0F+M77v+CPjiB+v/4ioBbM/CBvZf5GeP6NzJ8Lebl/t/8E8q2V8Y2Md0L/v8DvoFfpz7zw/IW81J91h/Vbod/6/Qz8MP5O7BfxRu4nkb+X+XkBf1h/JfRLz78KX//EVSDGX0T/idAfNqFQf+DzrYx39CfIL0146RQ+wcX488iHT3AlvKnDN1gg3/JhscL54A8TAYgvRr/JNxh/MvJHjr6H+Hzk6ye6Bfky8p/IQ7yb/LTqatDvA791GO/GHYzrugb5JvqbkvoS48fXHgV1hcBvcvIY345+9wT7xwm87XPMX3aL8BGoHaC9GOb78b3tdyhPXLwcgXtBkSdTUgPyP4evJxc0foWVzufT95vMZ1D9eeYhf0E+k/jLjbH1Bm9/GL3lA19/0F9fcvEPbtD/bjry/Yf8u5yK9yKPhfwbIV9A438fW/8g4kuC6u8u3AOh+r+Lo62EX/DFh56fU/LfqRfxRI+vmeca9F7CmxVlIj5/EPG2bkV8TjK+Dt/AC3h+BEr8llEJn5EX8W4II1jg90bmNzL+SMjHcxDyUv+heWz9pX7llY9Rv/rVfzD/oXnz9Y8iXKPRaDQajUaj0Wg0Go1Go9FoNBqNRqP5H+Uz47RA1g==:2820";
    }   
}
