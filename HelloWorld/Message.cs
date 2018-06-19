using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace HelloWorld
{
    class Message
    {

        public int date =  (int) DateTime.Now.DayOfWeek;
        public int heure = (int)DateTime.Now.Hour;
        
        public string message;

        public int Morning { get; set; }
        public int Afternoon { get; set; }
        private int Night { get; set; }
        

        public Message (int mornin = 8, int aftern = 13, int evening = 18)
        {
            SetMorning(mornin);
            SetAfter(aftern);
            SetNight(evening);
        }

        public void SetMorning(int mornin)
        {
            Morning = mornin;
        }

        public void SetNight(int evening)
        {
            Night = evening;
        }

        public void SetAfter(int aftern)
        {
            Afternoon = aftern;
        }

        public void GetMorning()
        {

        }

        public string GetHelloMessage()
        {
            if(date == 6 ||  (date == 5 && heure >=this.Night) || (date == 1 && heure <this.Morning))
            {
                message = "Bon Week End ";
            }
            else if (heure >=9 && heure < this.Afternoon)
            {
                message = "Bonjour ";
            }
            else if (heure >=this.Afternoon && heure < this.Night)
            {
                message = "Bon après midi ";
            }
            else
            {
                message = "Bonsoir ";
            }

            return message;
 
        }
        
       
    }
}
