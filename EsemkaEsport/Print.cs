using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace EsemkaEsport
{
    public partial class Print: Form
    {
        public Print(string match, string time, string totalTicket)
        {
            InitializeComponent();

            string html = "<!DOCTYPE html> <html lang=\"en\"> \r\n    <head> <meta charset=\"UTF-8\"> <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"> <title>Document</title> <style> .css{font - family: 'Times New Roman', Times, serif;text-align: center;\r\n            font-size: 20px;\r\n            margin: 10px 5px;\r\n        }\r\n    </style>\r\n</head>\r\n" +
            $"<body class=\"css\">\r\n    <span> ------------------------------</span><br>\r\n    <span> {match} </span> <br>\r\n    <span> Time : {time}</span> <br>\r\n    <span> Total Ticket : {totalTicket}</span> <br>\r\n    <span> ------------------------------</span><br>\r\n</body>\r\n</html>";
            webBrowser1.DocumentText = html ;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string print = "<script> window.print() </script>";
            string html = webBrowser1.DocumentText;
            webBrowser1.DocumentText = html + print;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
