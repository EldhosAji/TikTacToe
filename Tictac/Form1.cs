using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Tiktac
{
    public partial class Form1 : Form
    {

        List<string> list = new List<string>();

        //To store the status of the board
        int[,] board = new int[4, 4] {
           {-1, -1, -1, -1} ,
           {-1, -1, -1, -1} ,
           {-1, -1, -1, -1},
           {-1, -1, -1, -1}
        };

        int r, c;
        bool endgame = false;

        bool player = true;

        //Total time taken interms of seconds
        int timeTaken = 0;

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "p", "!",
        };


        

        public Form1()
        {
            InitializeComponent();

            //to store all i,j values in a list
            init();
        }

        private void init()
        {
            int i, j;
            //add i,j value to list in the currect order
            timer2.Start();
            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    list.Add(i + "," + j);
                }
            }
        }

        private bool result()
        {
            int k = 0;
            int p = player ? 1 : 0;
            
            ///check the 2d matrix for does player won the game or not 
            if(board[0,0]==p && board[1, 1] == p&& board[2, 2] == p&& board[3, 3] == p)
            {
                return true;
            }
            if (board[0, 3] == p && board[1, 2] == p && board[2, 1] == p && board[3, 0] == p)
            {
                return true;
            }

            while (k < 4)
            {
                if (board[k, 0] == p && board[k, 1] == p && board[k, 2] == p && board[k, 3] == p)
                {
                    return true;
                }
                if (board[0, k] == p && board[1, k] == p && board[2, k] == p && board[3, k] == p)
                {
                    return true;
                }
                k++;
            }

            return false;
        }


        private void label1_Click(object sender, EventArgs e)
        {

            if (endgame)
            {
                return;
            }

            //label ref
            Label clickLabel = sender as Label;
            //trimming the label name to get index 
            //Ex. label0 =>0
            //label1 =>1
            char[] charsToTrim3 = { 'L', 'a', 'b', 'e','l' };
            string name = clickLabel.Name.ToString();
            int index =Convert.ToInt32(name.Trim(charsToTrim3));

            //fetch the i,j value from list with index
            
            string[] v = (list[index]).Split(',');
            r = Convert.ToInt32(v[0]);
            c = Convert.ToInt32(v[1]);

            //check which player is playing
            if (player)
            {
                board[r, c] = 1;
            }
            else
            {
                board[r, c] = 0;
            }

            //result function will check the curret status
            //if the current playerwon the result will return true else false
            if (result())
            {
                MessageBox.Show((player?"spider ":"car ")+"won the game");
                endgame = true;
                Application.Restart();
            }
         

            player = !player;

            //based on player it will give an icon
            int icon = player ? 0 : 1;
            clickLabel.Text = icons[icon];
            timer1.Start();
            return;

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timeTaken++;
            timeCountLabel.Text = timeTaken.ToString();
        }
    }
}
