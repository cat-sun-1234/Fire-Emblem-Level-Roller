using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEFLevelUpper
{
    public partial class Form1 : Form
    {
        const int MAX_TRAINEE_HP = 30;
        const int MAX_TRAINEE_STAT = 10;
        const int MAX_UNPROMOTED_HP = 40;
        const int MAX_UNPROMOTED_STAT = 20;
        const int MAX_PROMOTED_HP = 60;
        const int MAX_PROMOTED_STAT = 30; //define max stats

        //const int MIN_HP_GROWTH = 40;
        //const int MIN_STR_GROWTH = 0;
        //const int MIN_MAG_GROWTH = 0;
        //const int MIN_STAT_GROWTH = 10; //in designer - for reference

        const int MAX_HP_GROWTH = 100;
        const int MAX_STAT_GROWTH = 70; //defines max growths

        const int EXTRA_GROWTH_PER_TIER = 5;

        const int MAX_TRAINEE_LEVEL = 5;
        const int MAX_LEVEL = 20;

        Random random = new Random(); //initialise random number generator

        Character c;

        //should include preferred stats and growth totals?

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLevel_Click(object sender, EventArgs e)
        {
            c = new Character();
            bool isValid = ValidityCheck();

            if (isValid)
            {
                if (numLevel.Value == numLevel.Maximum)
                {
                    MessageBox.Show("You have reached your level cap. Please promote instead.");
                }
                else
                {
                    LevelUp();
                }
            }
            //Validity check first , see if any of the values exceed max bounds
            //character is assigned values equal to those in the text boxes
            //level up, roll against 100count rn.
        }

        private void LevelUp()
        {    
            c.level++;
            numLevel.Value++;

            CheckStatUp(ref numHPStat, ref lbHPUp, ref c.hpStat, c.hpGrowth);
            CheckStatUp(ref numStrStat, ref lbStrUp, ref c.strStat, c.strGrowth);
            CheckStatUp(ref numMagStat, ref lbMagUp, ref c.magStat, c.magGrowth);
            CheckStatUp(ref numSklStat, ref lbSklUp, ref c.sklStat, c.sklGrowth);
            CheckStatUp(ref numSpdStat, ref lbSpdUp, ref c.spdStat, c.spdGrowth);
            CheckStatUp(ref numLucStat, ref lbLucUp, ref c.lucStat, c.lucGrowth);
            CheckStatUp(ref numDefStat, ref lbDefUp, ref c.defStat, c.defGrowth);
            CheckStatUp(ref numDefStat, ref lbDefUp, ref c.defStat, c.defGrowth);
        }

        private void CheckStatUp(ref NumericUpDown numStat, ref Label lbStatUp, ref int CharacterStat, int CharacterGrowth)
        {
            int StatGrowth = 0;

            if (numStat.Value == numStat.Maximum)
            {
                numStat.ForeColor = Color.Green;
                lbStatUp.Visible = true;
                lbStatUp.ForeColor = Color.Green;
                lbStatUp.Text = "MAX";
            }
            else
            {
                StatGrowth = StatUp(CharacterGrowth);
                if (StatGrowth > 0)
                {
                    lbStatUp.Visible = true;
                    lbStatUp.ForeColor = Color.Green;
                    lbStatUp.Text = "+" + StatGrowth;
                    CharacterStat += StatGrowth;
                    numStat.Value += StatGrowth;
                    if (numStat.Value >= numStat.Maximum)
                    {
                        numStat.Value = numStat.Maximum;
                        numStat.ForeColor = Color.Green;
                    }
                }
                else
                    lbStatUp.Visible = false;
            }
        }

        private int StatUp(int growth)
        {
            int StatGrowth = 0;
            int ranNum;
            while (growth > 100)
            {
                StatGrowth++;
                growth -= 100;
            }
            ranNum = random.Next(0, 100);
            if (growth > ranNum)
                StatGrowth++;
            return StatGrowth;
        } //every 100 growths gets instant +1. all others roll vs rng of max 100

        private bool ValidityCheck()
        {
            if (tbName.Text != String.Empty)
            {
                c.name = tbName.Text;
            }
            else
            {
                MessageBox.Show("Please enter a name."); //if there's no name, it's invalid
                return false;
            }

            String tier;
            if (cbTier.Text != String.Empty) //checks if combo box Tier is not empty
            {
                tier = cbTier.Text;
                if (tier != "Trainee" && tier != "First Class" && tier != "Promoted")
                {
                    MessageBox.Show("Your chosen tier is invalid.");    //error message if chosen tier isn't one of the three (Note: Shouldn't appear)
                    return false;
                }
                else
                {
                    c.tier = tier;
                }
            }
            else
            {
                MessageBox.Show("Please select a tier");
                return false;
            }

            if (numLevel.Value < numLevel.Minimum || numLevel.Value > numLevel.Maximum) //if entered level exceeds max  and mins (fixed when selecting tiers) 
            {
                MessageBox.Show("Please choose a level between " + numLevel.Minimum + " and " + numLevel.Maximum); //print error and invalidate (shouldn't happen but just in case)
                return false;
            }
            else
                c.level = Convert.ToInt32(numLevel.Value); //if passed, sent to character

            if (numHPStat.Value < numHPStat.Minimum || numHPStat.Value > numHPStat.Maximum)
            {
                MessageBox.Show("Please choose a max HP between " + numHPStat.Minimum + " and " + numHPStat.Maximum);
                return false;
            }
            else
                c.hpStat = Convert.ToInt32(numHPStat.Value);

            if (numStrStat.Value < numStrStat.Minimum || numStrStat.Value > numStrStat.Maximum)
            {
                MessageBox.Show("Please choose a strength stat between " + numStrStat.Minimum + " and " + numStrStat.Maximum);
                return false;
            }
            else
                c.strStat = Convert.ToInt32(numStrStat.Value);

            if (numMagStat.Value < numMagStat.Minimum || numMagStat.Value > numMagStat.Maximum)
            {
                MessageBox.Show("Please choose a magic stat between " + numMagStat.Minimum + " and " + numMagStat.Maximum);
                return false;
            }
            else
                c.magStat = Convert.ToInt32(numMagStat.Value);

            if (numSklStat.Value < numSklStat.Minimum || numSklStat.Value > numSklStat.Maximum)
            {
                MessageBox.Show("Please choose a skill stat between " + numSklStat.Minimum + " and " + numSklStat.Maximum);
                return false;
            }
            else
                c.sklStat = Convert.ToInt32(numSklStat.Value);

            if (numSpdStat.Value < numSpdStat.Minimum || numSpdStat.Value > numSpdStat.Maximum)
            {
                MessageBox.Show("Please choose a speed stat between " + numSpdStat.Minimum + " and " + numSpdStat.Maximum);
                return false;
            }
            else
                c.spdStat = Convert.ToInt32(numSpdStat.Value);

            if (numLucStat.Value < numLucStat.Minimum || numLucStat.Value > numLucStat.Maximum)
            {
                MessageBox.Show("Please choose a luck stat between " + numLucStat.Minimum + " and " + numLucStat.Maximum);
                return false;
            }
            else
                c.lucStat = Convert.ToInt32(numLucStat.Value);

            if (numDefStat.Value < numDefStat.Minimum || numDefStat.Value > numDefStat.Maximum)
            {
                MessageBox.Show("Please choose a defence stat between " + numDefStat.Minimum + " and " + numDefStat.Maximum);
                return false;
            }
            else
                c.defStat = Convert.ToInt32(numDefStat.Value);

            if (numResStat.Value < numResStat.Minimum || numResStat.Value > numResStat.Maximum)
            {
                MessageBox.Show("Please choose a resistance stat between " + numResStat.Minimum + " and " + numResStat.Maximum);
                return false;
            }
            else
                c.resStat = Convert.ToInt32(numResStat.Value);

            if (numHPGrowth.Value < numHPGrowth.Minimum || numHPGrowth.Value > numHPGrowth.Maximum)
            {
                MessageBox.Show("Please choose a HP growth between " + numHPGrowth.Minimum + " and " + numHPGrowth.Maximum);
                return false;
            }
            else
                c.hpGrowth = Convert.ToInt32(numHPGrowth.Value);

            if (numStrGrowth.Value < numStrGrowth.Minimum || numStrGrowth.Value > numStrGrowth.Maximum)
            {
                MessageBox.Show("Please choose a strength growth between " + numStrGrowth.Minimum + " and " + numStrGrowth.Maximum);
                return false;
            }
            else
                c.strGrowth = Convert.ToInt32(numStrGrowth.Value);

            if (numMagGrowth.Value < numMagGrowth.Minimum || numMagGrowth.Value > numMagGrowth.Maximum)
            {
                MessageBox.Show("Please choose a magic growth between " + numMagGrowth.Minimum + " and " + numMagGrowth.Maximum);
                return false;
            }
            else
                c.magGrowth = Convert.ToInt32(numMagGrowth.Value);

            if (numSklGrowth.Value < numSklGrowth.Minimum || numSklGrowth.Value > numSklGrowth.Maximum)
            {
                MessageBox.Show("Please choose a skill growth between " + numSklGrowth.Minimum + " and " + numSklGrowth.Maximum);
                return false;
            }
            else
                c.sklGrowth = Convert.ToInt32(numSklGrowth.Value);

            if (numSpdGrowth.Value < numSpdGrowth.Minimum || numSpdGrowth.Value > numSpdGrowth.Maximum)
            {
                MessageBox.Show("Please choose a speed growth between " + numSpdGrowth.Minimum + " and " + numSpdGrowth.Maximum);
                return false;
            }
            else
                c.spdGrowth = Convert.ToInt32(numSpdGrowth.Value);

            if (numLucGrowth.Value < numLucGrowth.Minimum || numLucGrowth.Value > numLucGrowth.Maximum)
            {
                MessageBox.Show("Please choose a luck growth between " + numLucGrowth.Minimum + " and " + numLucGrowth.Maximum);
                return false;
            }
            else
                c.lucGrowth = Convert.ToInt32(numLucGrowth.Value);

            if (numDefGrowth.Value < numDefGrowth.Minimum || numDefGrowth.Value > numDefGrowth.Maximum)
            {
                MessageBox.Show("Please choose a defence growth between " + numDefGrowth.Minimum + " and " + numDefGrowth.Maximum);
                return false;
            }
            else
                c.defGrowth = Convert.ToInt32(numDefGrowth.Value);

            if (numResGrowth.Value < numResGrowth.Minimum || numResGrowth.Value > numResGrowth.Maximum)
            {
                MessageBox.Show("Please choose a resistance growth between " + numResGrowth.Minimum + " and " + numResGrowth.Maximum);
                return false;
            }
            else
                c.resGrowth = Convert.ToInt32(numResGrowth.Value);

            return true;
    
        } //checks validity of entered data and exports to character

        private void cbTier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedTier();
        } 

        private void ChangedTier()
        {
            if (cbTier.Text == "Trainee")
            {
                numLevel.Maximum = MAX_TRAINEE_LEVEL;
                if (numLevel.Value > MAX_TRAINEE_LEVEL)
                    numLevel.Value = MAX_TRAINEE_LEVEL;

                numHPStat.Maximum = MAX_TRAINEE_HP;
                if (numHPStat.Value > MAX_TRAINEE_HP)
                    numHPStat.Value = MAX_TRAINEE_HP;

                numStrStat.Maximum = MAX_TRAINEE_STAT;
                if (Convert.ToInt32(numStrStat.Value) > MAX_TRAINEE_STAT)
                    numStrStat.Value = MAX_TRAINEE_STAT;

                numMagStat.Maximum = MAX_TRAINEE_STAT;
                if (numMagStat.Value > MAX_TRAINEE_STAT)
                    numMagStat.Value = MAX_TRAINEE_STAT;

                numSklStat.Maximum = MAX_TRAINEE_STAT;
                if (numSklStat.Value > MAX_TRAINEE_STAT)
                    numSklStat.Value = MAX_TRAINEE_STAT;

                numSpdStat.Maximum = MAX_TRAINEE_STAT;
                if (numSpdStat.Value > MAX_TRAINEE_STAT)
                    numSpdStat.Value = MAX_TRAINEE_STAT;

                numLucStat.Maximum = MAX_TRAINEE_STAT;
                if (numLucStat.Value > MAX_TRAINEE_STAT)
                    numLucStat.Value = MAX_TRAINEE_STAT;

                numDefStat.Maximum = MAX_TRAINEE_STAT;
                if (numDefStat.Value > MAX_TRAINEE_STAT)
                    numDefStat.Value = MAX_TRAINEE_STAT;

                numResStat.Maximum = MAX_TRAINEE_STAT;
                if (numResStat.Value > MAX_TRAINEE_STAT)
                    numResStat.Value = MAX_TRAINEE_STAT;

                int maxGrowth = MAX_HP_GROWTH;
                numHPGrowth.Maximum = maxGrowth;
                if (numHPGrowth.Value > maxGrowth)
                    numHPGrowth.Value = maxGrowth;

                maxGrowth = MAX_STAT_GROWTH;
                numStrGrowth.Maximum = maxGrowth;
                if (numStrGrowth.Value > maxGrowth)
                    numStrGrowth.Value = maxGrowth;
                numMagGrowth.Maximum = maxGrowth;
                if (numMagGrowth.Value > maxGrowth)
                    numMagGrowth.Value = maxGrowth;
                numSklGrowth.Maximum = maxGrowth;
                if (numSklGrowth.Value > maxGrowth)
                    numSklGrowth.Value = maxGrowth;
                numSpdGrowth.Maximum = maxGrowth;
                if (numSpdGrowth.Value > maxGrowth)
                    numSpdGrowth.Value = maxGrowth;
                numLucGrowth.Maximum = maxGrowth;
                if (numLucGrowth.Value > maxGrowth)
                    numLucGrowth.Value = maxGrowth;
                numDefGrowth.Maximum = maxGrowth;
                if (numDefGrowth.Value > maxGrowth)
                    numDefGrowth.Value = maxGrowth;
                numResGrowth.Maximum = maxGrowth;
                if (numResGrowth.Value > maxGrowth)
                    numResGrowth.Value = maxGrowth;

            }
            else if (cbTier.Text == "First Class")
            {
                numLevel.Maximum = MAX_LEVEL;
                if (numLevel.Value > MAX_LEVEL)
                    numLevel.Value = MAX_LEVEL;

                numHPStat.Maximum = MAX_UNPROMOTED_HP;
                if (numHPStat.Value > MAX_UNPROMOTED_HP)
                    numHPStat.Value = MAX_UNPROMOTED_HP;

                numStrStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numStrStat.Value > MAX_UNPROMOTED_STAT)
                    numStrStat.Value = MAX_UNPROMOTED_STAT;

                numMagStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numMagStat.Value > MAX_UNPROMOTED_STAT)
                    numMagStat.Value = MAX_UNPROMOTED_STAT;

                numSklStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numSklStat.Value > MAX_UNPROMOTED_STAT)
                    numSklStat.Value = MAX_UNPROMOTED_STAT;

                numSpdStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numSpdStat.Value > MAX_UNPROMOTED_STAT)
                    numSpdStat.Value = MAX_UNPROMOTED_STAT;

                numLucStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numLucStat.Value > MAX_UNPROMOTED_STAT)
                    numLucStat.Value = MAX_UNPROMOTED_STAT;

                numDefStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numDefStat.Value > MAX_UNPROMOTED_STAT)
                    numDefStat.Value = MAX_UNPROMOTED_STAT;

                numResStat.Maximum = MAX_UNPROMOTED_STAT;
                if (numResStat.Value > MAX_UNPROMOTED_STAT)
                    numResStat.Value = MAX_UNPROMOTED_STAT;

                int maxGrowth = MAX_HP_GROWTH + EXTRA_GROWTH_PER_TIER;
                numHPGrowth.Maximum = maxGrowth;
                if (numHPGrowth.Value > maxGrowth)
                    numHPGrowth.Value = maxGrowth;

                maxGrowth = MAX_STAT_GROWTH + EXTRA_GROWTH_PER_TIER;
                numStrGrowth.Maximum = maxGrowth;
                if (numStrGrowth.Value > maxGrowth)
                    numStrGrowth.Value = maxGrowth;
                numMagGrowth.Maximum = maxGrowth;
                if (numMagGrowth.Value > maxGrowth)
                    numMagGrowth.Value = maxGrowth;
                numSklGrowth.Maximum = maxGrowth;
                if (numSklGrowth.Value > maxGrowth)
                    numSklGrowth.Value = maxGrowth;
                numSpdGrowth.Maximum = maxGrowth;
                if (numSpdGrowth.Value > maxGrowth)
                    numSpdGrowth.Value = maxGrowth;
                numLucGrowth.Maximum = maxGrowth;
                if (numLucGrowth.Value > maxGrowth)
                    numLucGrowth.Value = maxGrowth;
                numDefGrowth.Maximum = maxGrowth;
                if (numDefGrowth.Value > maxGrowth)
                    numDefGrowth.Value = maxGrowth;
                numResGrowth.Maximum = maxGrowth;
                if (numResGrowth.Value > maxGrowth)
                    numResGrowth.Value = maxGrowth;

            }
            else if (cbTier.Text == "Promoted")
            {
                numLevel.Maximum = MAX_LEVEL;
                if (numLevel.Value > MAX_LEVEL)
                    numLevel.Value = MAX_LEVEL;

                numHPStat.Maximum = MAX_PROMOTED_HP;
                if (numHPStat.Value > MAX_PROMOTED_HP)
                    numHPStat.Value = MAX_PROMOTED_HP;

                numStrStat.Maximum = MAX_PROMOTED_STAT;
                if (numStrStat.Value > MAX_PROMOTED_STAT)
                    numStrStat.Value = MAX_PROMOTED_STAT;

                numMagStat.Maximum = MAX_PROMOTED_STAT;
                if (numMagStat.Value > MAX_PROMOTED_STAT)
                    numMagStat.Value = MAX_PROMOTED_STAT;

                numSklStat.Maximum = MAX_PROMOTED_STAT;
                if (numSklStat.Value > MAX_PROMOTED_STAT)
                    numSklStat.Value = MAX_PROMOTED_STAT;

                numSpdStat.Maximum = MAX_PROMOTED_STAT;
                if (numSpdStat.Value > MAX_PROMOTED_STAT)
                    numSpdStat.Value = MAX_PROMOTED_STAT;

                numLucStat.Maximum = MAX_PROMOTED_STAT;
                if (numLucStat.Value > MAX_PROMOTED_STAT)
                    numLucStat.Value = MAX_PROMOTED_STAT;

                numDefStat.Maximum = MAX_PROMOTED_STAT;
                if (numDefStat.Value > MAX_PROMOTED_STAT)
                    numDefStat.Value = MAX_PROMOTED_STAT;

                numResStat.Maximum = MAX_PROMOTED_STAT;
                if (numResStat.Value > MAX_PROMOTED_STAT)
                    numResStat.Value = MAX_PROMOTED_STAT;


                int maxGrowth = MAX_HP_GROWTH + EXTRA_GROWTH_PER_TIER * 2;
                numHPGrowth.Maximum = maxGrowth;
                if (numHPGrowth.Value > maxGrowth)
                    numHPGrowth.Value = maxGrowth;

                maxGrowth = MAX_STAT_GROWTH + EXTRA_GROWTH_PER_TIER * 2;
                numStrGrowth.Maximum = maxGrowth;
                if (numStrGrowth.Value > maxGrowth)
                    numStrGrowth.Value = maxGrowth;
                numMagGrowth.Maximum = maxGrowth;
                if (numMagGrowth.Value > maxGrowth)
                    numMagGrowth.Value = maxGrowth;
                numSklGrowth.Maximum = maxGrowth;
                if (numSklGrowth.Value > maxGrowth)
                    numSklGrowth.Value = maxGrowth;
                numSpdGrowth.Maximum = maxGrowth;
                if (numSpdGrowth.Value > maxGrowth)
                    numSpdGrowth.Value = maxGrowth;
                numLucGrowth.Maximum = maxGrowth;
                if (numLucGrowth.Value > maxGrowth)
                    numLucGrowth.Value = maxGrowth;
                numDefGrowth.Maximum = maxGrowth;
                if (numDefGrowth.Value > maxGrowth)
                    numDefGrowth.Value = maxGrowth;
                numResGrowth.Maximum = maxGrowth;
                if (numResGrowth.Value > maxGrowth)
                    numResGrowth.Value = maxGrowth;
            }
            else
                MessageBox.Show("Your chosen tier is invalid."); //error message if chosen tier isn't one of the three (Note: Shouldn't appear)
        } //defines max levels, stats and growth depending on tier chosen (mins are coded in the design and shouldn't change)
        //new method to be called when loading new file

        private void saveCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = new Character();
            bool isValid = ValidityCheck();

            if (isValid)
            {
                String saveString = c.name + "," + c.tier + "," + c.level 
                    + "," + c.hpStat + "," + c.hpGrowth + "," + c.strStat + "," + c.strGrowth 
                    + "," + c.magStat + "," + c.magGrowth + "," + c.sklStat + "," + c.sklGrowth 
                    + "," + c.spdStat + "," + c.spdGrowth + "," + c.lucStat + "," + c.lucGrowth
                    + "," + c.defStat + "," + c.defGrowth + "," + c.resStat + "," + c.resGrowth + ",/" ;
                String fileName = c.name;
                System.IO.Directory.CreateDirectory("characters");
                System.IO.File.WriteAllText(@".\characters\" + fileName + ".txt", saveString);
                MessageBox.Show("Character File Saved");
            }
            //checks validity before writing to new file
        } //exports character file to external file - preferably <character name>.txt //'/' is end of line, ',' is end of stat

        private void loadCharacterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            System.IO.StreamReader reader;

            if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                reader = new System.IO.StreamReader(fileDialog.FileName);
                String characterString = reader.ReadLine();
                String[] charData = characterString.Split(','); //split string
                //and now to write them in
                tbName.Text = charData[0];
                cbTier.Text = charData[1];
                ChangedTier();
                numLevel.Value = Int32.Parse(charData[2]);
                numHPStat.Value = Int32.Parse(charData[3]);
                numHPGrowth.Value = Int32.Parse(charData[4]);
                numStrStat.Value = Int32.Parse(charData[5]);
                numStrGrowth.Value = Int32.Parse(charData[6]);
                numMagStat.Value = Int32.Parse(charData[7]);
                numMagGrowth.Value = Int32.Parse(charData[8]);
                numSklStat.Value = Int32.Parse(charData[9]);
                numSklGrowth.Value = Int32.Parse(charData[10]);
                numSpdStat.Value = Int32.Parse(charData[11]);
                numSpdGrowth.Value = Int32.Parse(charData[12]);
                numLucStat.Value = Int32.Parse(charData[13]);
                numLucGrowth.Value = Int32.Parse(charData[14]);
                numDefStat.Value = Int32.Parse(charData[15]);
                numDefGrowth.Value = Int32.Parse(charData[16]);
                numResStat.Value = Int32.Parse(charData[17]);
                numResGrowth.Value = Int32.Parse(charData[18]);
                reader.Close(); //close the stream reader
            }

            //loaded data from file goes here. data must be loaded before validity check can start
            bool isValid = ValidityCheck();

            if (isValid)
            {
                MessageBox.Show("Character File Loaded");
            }
            else
            {
                ResetForm();
                MessageBox.Show("Error in Character File"); //wipes the form

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        } 

        private void ResetForm()
        {
            tbName.Text = String.Empty;
            cbTier.SelectedText = String.Empty;
            cbTier.Text = "Tier";
            numLevel.Value = numLevel.Minimum;
            numHPStat.Value = numHPStat.Minimum;
            numStrStat.Value = numStrStat.Minimum;
            numMagStat.Value = numMagStat.Minimum;
            numSklStat.Value = numSklStat.Minimum;
            numSpdStat.Value = numSpdStat.Minimum;
            numLucStat.Value = numLucStat.Minimum;
            numDefStat.Value = numDefStat.Minimum;
            numResStat.Value = numResStat.Minimum;

            numHPStat.ForeColor = SystemColors.WindowText;
            numStrStat.ForeColor = SystemColors.WindowText;
            numMagStat.ForeColor = SystemColors.WindowText;
            numSklStat.ForeColor = SystemColors.WindowText;
            numSpdStat.ForeColor = SystemColors.WindowText;
            numLucStat.ForeColor = SystemColors.WindowText;
            numDefStat.ForeColor = SystemColors.WindowText;
            numResStat.ForeColor = SystemColors.WindowText;

            numHPGrowth.Value = numHPGrowth.Minimum;
            numStrGrowth.Value = numStrGrowth.Minimum;
            numMagGrowth.Value = numMagGrowth.Minimum;
            numSklGrowth.Value = numSklGrowth.Minimum;
            numSpdGrowth.Value = numSpdGrowth.Minimum;
            numLucGrowth.Value = numLucGrowth.Minimum;
            numDefGrowth.Value = numDefGrowth.Minimum;
            numResGrowth.Value = numResGrowth.Minimum;

            lbHPUp.Visible = false;
            lbStrUp.Visible = false;
            lbMagUp.Visible = false;
            lbSklUp.Visible = false;
            lbSpdUp.Visible = false;
            lbLucUp.Visible = false;
            lbDefUp.Visible = false;
            lbResUp.Visible = false;
        } //resets program for next entry

        private void numHPStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numHPStat);
        }

        private void numStrStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numStrStat);
        }

        private void numMagStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numMagStat);
        }

        private void numSklStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numSklStat);
        }

        private void numSpdStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numSpdStat);
        }

        private void numLucStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numLucStat);
        }

        private void numDefStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numDefStat);
        }

        private void numResStat_ValueChanged(object sender, EventArgs e)
        {
            NumMaxColorChange(ref numResStat);
        }

        private void NumMaxColorChange(ref NumericUpDown numStat)
        {
            if (numStat.Value == numStat.Maximum)
                numStat.ForeColor = Color.DarkGreen;
            else
                numStat.ForeColor = SystemColors.WindowText;
        } //changes color of stats to green if max is reached
        //maybe do this to level and growths too?
    }
}
