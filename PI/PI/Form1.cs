using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PI
{
    public partial class Form1 : Form
    {
        RandomGenerator random = new RandomGenerator();
        double inCircle = 0;
        long n = 0;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = Convert.ToInt64(number.Text);
            start();
            double res = inCircle*4/(double)n;
            result.Text = res.ToString();
        }

        private void start() {
            double x, y;
            inCircle = 0;
		    for(long i = 0; i < n; i++){
			    x = random.GetRandomNumber();
			    y = random.GetRandomNumber();
			
			    if(x*x + y*y <= 1.0){
				    inCircle++;
			    }
		    }
	    }
    }
}
