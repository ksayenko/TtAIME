using Read3mb;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TetratechTools;

namespace RunManatea
{
    public struct APP_Config_Keys
    {
        public static string _3mbIniFile = "3mbIniFile";
        public static string gridAsciiFolder = "gridAsciiFolder";
        public static string gridBinaryFile = "gridBinaryFile";
        public static string pileCSV = "pileCSV";
        public static string dbSeaData = "dbSeaData";
        public static string output = "output";
        public static string gridStep = "gridStep";

        public static string soundSourceType = "sourceType";
        public static string soundMetric = "soundMetric";
        public static string TtAIME_Marine_Mammal_Dictionary_file ="TtAIME_Marine_Mammal_Dictionary";

    }
    public partial class Settings : Form
    {
        AppSettings appSet;

        private LogHandler logHandler;

        public void Initialize2()
        {
            InitializeComponent();
            //table panel layout keep disspapera from the controls
            appSet = new AppSettings();   

            bool badded = false;
            foreach (Control c in this.Controls)
            {
                if (c.Name == tableLayoutPanel1.Name)
                {
                    badded = true; break;
                }
            }
            if (!badded)
                this.Controls.Add(tableLayoutPanel1);

        }
        public Settings(LogHandler lh = null)
        {
            Initialize2();
            if (lh == null)
                logHandler = new LogHandler();
            else
                logHandler = lh;

            if (appSet == null)
                appSet = new AppSettings();

            appSet.ReadSettings(logHandler);

            labelTextBox_3mb.LabelText = "Location " + APP_Config_Keys._3mbIniFile;
            labelTextBox_3mb.TextBoxText = A3mbIniFile;
            labelTextBox_3mb.IsFile = true;

            labelTextBox_gridcsv.LabelText = "Location " + APP_Config_Keys.gridAsciiFolder;
            labelTextBox_gridcsv.TextBoxText = GridCSV;
            labelTextBox_gridcsv.IsFile = true;

            labelTextBox_GridBinary.LabelText = "Location " + APP_Config_Keys.gridBinaryFile;
            labelTextBox_GridBinary.TextBoxText = GridBinary;
            labelTextBox_GridBinary.IsFile = true;

            labelTextBox_pileCSV.LabelText = "Location " + APP_Config_Keys.pileCSV;
            labelTextBox_pileCSV.TextBoxText = PileCSV;
            labelTextBox_pileCSV.IsFile = true;

            labelTextBox_dbSeaData.LabelText = "Location " + APP_Config_Keys.dbSeaData;
            labelTextBox_dbSeaData.TextBoxText = DbSeaData;
            labelTextBox_dbSeaData.IsFolder = true;
            labelTextBox_dbSeaData.IsFile = false;

            labelTextBox_Out.LabelText = "Location " + APP_Config_Keys.output;
            labelTextBox_Out.TextBoxText = Output;
            labelTextBox_Out.IsFolder = true;
            labelTextBox_Out.IsFile = false;



        }

        public string A3mbIniFile 
        { get => appSet.A3mbIniFile; set => appSet.A3mbIniFile = value; }
        public string GridCSV { get => appSet.GridAsciiFolder; set => appSet.GridAsciiFolder = value; }
        public string GridBinary { get => appSet.GridBinary; set => appSet.GridBinary = value; }
        public string PileCSV { get => appSet.PileCSV; set => appSet.PileCSV = value; }
        public string DbSeaData { get => appSet.DbSeaData; set => appSet.DbSeaData = value; }
        public string Output { get => appSet.Output; set => appSet.Output = value; }

    


        public bool ContainsInArray(string[] arraystring, string key)
        {
            bool rv = false;
            if (arraystring != null && key != null && key != "")
            {
                foreach (string s in arraystring)
                {
                    if (s.Equals(key))
                        return true;
                }
            }
            return rv;
        }


        public void SaveSetting(string sKey, object sValue, LogHandler lh = null)
        {
            if (sKey != null && sKey != "" && sValue != null)
                try
                {
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var appSettings = configFile.AppSettings.Settings;
                    if (appSettings[sKey] == null)
                    {
                        appSettings.Add(sKey, sValue.ToString());
                    }
                    else
                    {
                        appSettings[sKey].Value = sValue.ToString();
                    }
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                    if (lh != null)
                        lh.LogWarning("The setting " + sKey + " has been saved.");
                }
                catch (Exception ex)
                {
                    if (lh != null)
                    {
                        lh.LogError(ex, MethodBase.GetCurrentMethod());
                        /*
                        lh.LogError("Error in Save Settings : " + ex.ToString());
                        if (ex.InnerException != null)
                            lh.LogError(ex.InnerException.ToString());
                        */
                    }
                }
        }

        public void SaveOutputFolderToConfigFile()
        {
            SaveSetting(APP_Config_Keys.output, Output, logHandler);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PileCSV = labelTextBox_pileCSV.TextBoxText;
            GridBinary = labelTextBox_GridBinary.TextBoxText;
            A3mbIniFile = labelTextBox_3mb.TextBoxText;
            GridCSV = labelTextBox_gridcsv.TextBoxText; ;
            DbSeaData = labelTextBox_dbSeaData.TextBoxText;
            Output = labelTextBox_Out.TextBoxText;
            SaveSetting(APP_Config_Keys.pileCSV, labelTextBox_pileCSV.TextBoxText, logHandler);
            SaveSetting(APP_Config_Keys._3mbIniFile, labelTextBox_3mb.TextBoxText, logHandler);
            SaveSetting(APP_Config_Keys.gridAsciiFolder, labelTextBox_gridcsv.TextBoxText, logHandler);
            SaveSetting(APP_Config_Keys.gridBinaryFile, labelTextBox_GridBinary.TextBoxText, logHandler);
            SaveSetting(APP_Config_Keys.dbSeaData, labelTextBox_dbSeaData.TextBoxText, logHandler);
            SaveSetting(APP_Config_Keys.output, labelTextBox_Out.TextBoxText, logHandler);

            this.Close();
        }

        private void labelTextBox_pileCSV_Load(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {

            if (PileCSV == null)
                PileCSV = labelTextBox_pileCSV.TextBoxText = "";

            if (GridBinary == null)
                GridBinary = labelTextBox_GridBinary.TextBoxText = "";
            if (A3mbIniFile == null)
                A3mbIniFile = labelTextBox_3mb.TextBoxText = "";
            if (GridCSV == null)
            {
                GridCSV = labelTextBox_gridcsv.TextBoxText = "";

            }
            if (DbSeaData == null)
            {
                DbSeaData = labelTextBox_dbSeaData.TextBoxText = "";

            }
            if (Output == null)
            {
                Output = labelTextBox_Out.TextBoxText = "";

            }
            tableLayoutPanel1.Visible = true;
            labelTextBox_dbSeaData.Visible = labelTextBox_dbSeaData.Visible = true;

        }

        private void labelTextBox_Out_Load(object sender, EventArgs e)
        {

        }
    }

    public class AppSettings
    {
        private string _3mbIniFile="";
        private string gridAsciiFolder = "";
        private string gridBinary = "";
        private string pileCSV = "";
        private string dbSeaData = "";
        private string output = "";
        private double dGridStep = 1;
        private SoundSourceType soundSourceType = SoundSourceType.Impulsive;
        private SoundSourceMetric soundMetric = SoundSourceMetric.UNKNOWN;
        private string ttAIME_Marine_Mammal_Dictionary_file = "";

        LogHandler lh;

        public string A3mbIniFile { get => _3mbIniFile; set => _3mbIniFile = value; }
        public string GridAsciiFolder { get => gridAsciiFolder; set => gridAsciiFolder = value; }
        public string GridBinary { get => gridBinary; set => gridBinary = value; }
        public string PileCSV { get => pileCSV; set => pileCSV = value; }
        public string DbSeaData { get => dbSeaData; set => dbSeaData = value; }
        public string Output { get => output; set => output = value; }
        public double DGridStep { get => dGridStep; set => dGridStep = value; }
        public string TtAIME_Marine_Mammal_Dictionary_file { get => ttAIME_Marine_Mammal_Dictionary_file; set => ttAIME_Marine_Mammal_Dictionary_file = value; }

        public AppSettings() { lh =new LogHandler(); }

        public void ReadSettings(LogHandler lh = null)
        {
            if (lh == null)
                lh = new LogHandler();

            string[] keys = null;
            try
            {
                keys = ConfigurationManager.AppSettings.AllKeys;
            }
            catch (Exception ex)
            {
                lh.LogError(ex, MethodBase.GetCurrentMethod());
            }

                try
            {
                if (ContainsInArray(keys, APP_Config_Keys._3mbIniFile))
                    A3mbIniFile = ConfigurationManager.AppSettings[APP_Config_Keys._3mbIniFile].ToString().Trim();
                //if (_3mbIniFile == null || _3mbIniFile == "")
                //    throw new Exception("no catalog in config file, use default ");
            }
            catch (Exception ex)
            {
                lh.LogError(ex, MethodBase.GetCurrentMethod());
                A3mbIniFile = "NA";
            }



            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.gridAsciiFolder))
                    GridAsciiFolder = ConfigurationManager.AppSettings[APP_Config_Keys.gridAsciiFolder].ToString();
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (gridCSV): ");
                lh.LogError(ex);
            }

            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.gridBinaryFile))
                    gridBinary = ConfigurationManager.AppSettings[APP_Config_Keys.gridBinaryFile].ToString();
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (gridBinary): ");
                lh.LogError(ex);
            }

            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.pileCSV))
                    pileCSV = ConfigurationManager.AppSettings[APP_Config_Keys.pileCSV].ToString();
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (pileCSV): ");
                lh.LogError(ex);
            }

            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.dbSeaData))
                    dbSeaData = ConfigurationManager.AppSettings[APP_Config_Keys.dbSeaData].ToString();
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (dbSeaData): ");
                lh.LogError(ex);
            }

            if (dbSeaData == null)
                dbSeaData = "C:\\Users";

            try
            {
                string temp = "1.0";
                double d = 1.0;
                if (ContainsInArray(keys, APP_Config_Keys.gridStep))
                    temp = ConfigurationManager.AppSettings[APP_Config_Keys.gridStep].ToString();

                Double.TryParse(temp, out d);
                DGridStep = d;
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (d step): ");
                lh.LogError(ex);
            }

            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.output))
                    output = ConfigurationManager.AppSettings[APP_Config_Keys.output].ToString();

                if (output == null || output == "" )
                {
                   
                    FileInfo fi = new FileInfo(this._3mbIniFile);
                    output = fi.Directory
                        + Path.DirectorySeparatorChar.ToString()
                        + fi.Name.Replace(".", "");
                }
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (output): ");
                lh.LogError(ex);
                output = "";
            }

            //new
             try
            {
                if (ContainsInArray(keys, APP_Config_Keys.soundMetric))
                {
                   string temp = (ConfigurationManager.AppSettings[APP_Config_Keys.soundMetric]);
                    soundMetric = temp.ToSoundSourceMetricFromDescription();
                }
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (soundMetric): ");
                lh.LogError(ex);
             
            }

            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.soundSourceType))
                {
                    string temp = (ConfigurationManager.AppSettings[APP_Config_Keys.soundSourceType]);
                    soundSourceType = temp.ToSoundSourceTypeFromDescription();
                }
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (soundSourceType): ");
                lh.LogError(ex);
       
            }

            //new
            try
            {
                if (ContainsInArray(keys, APP_Config_Keys.TtAIME_Marine_Mammal_Dictionary_file))
                {
                    ttAIME_Marine_Mammal_Dictionary_file = (ConfigurationManager.AppSettings[APP_Config_Keys.TtAIME_Marine_Mammal_Dictionary_file]);
                
                }
            }
            catch (Exception ex)
            {
                lh.LogError(" Error in ReadSettings (TtAIME_Marine_Mammal_Dictionary_file): ");
                lh.LogError(ex);
                ttAIME_Marine_Mammal_Dictionary_file = "";
            }



        }


        public void SaveSettings(LogHandler lh = null)
        {
            if (lh == null)
                lh = new LogHandler();
            if (A3mbIniFile != null && A3mbIniFile != "")
                Save3MBMovemetFileToConfigFile();
            if (output != null && output != "")
                SaveOutputFolderToConfigFile();
            if (GridAsciiFolder != null && GridAsciiFolder != "")
                SaveAsciiFolderToConfigFile();
            if (PileCSV != null && PileCSV != "")
                SaveSetting(APP_Config_Keys.pileCSV, PileCSV, lh);
            if (DGridStep >0)
                SaveSetting(APP_Config_Keys.gridStep, DGridStep, lh);

            SaveSetting(APP_Config_Keys.soundSourceType, soundSourceType, lh);
            SaveSetting(APP_Config_Keys.soundMetric, soundMetric, lh);

            if (GridBinary != null && GridBinary != "")
                SaveSetting(APP_Config_Keys.gridBinaryFile, GridBinary, lh);



        }
        public bool ContainsInArray(string[] arraystring, string key)
        {
            bool rv = false;
            if (arraystring != null && key != null && key != "")
            {
                foreach (string s in arraystring)
                {
                    if (s.Equals(key))
                        return true;
                }
            }
            return rv;
        }


        public void SaveSetting(string sKey, object sValue, LogHandler lh = null)
        {
            if (sKey != null && sKey != "" && sValue != null)
                try
                {
                    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var appSettings = configFile.AppSettings.Settings;
                    if (appSettings[sKey] == null)
                    {
                        appSettings.Add(sKey, sValue.ToString());
                    }
                    else
                    {
                        appSettings[sKey].Value = sValue.ToString();
                    }
                    configFile.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                    if (lh != null)
                        lh.LogWarning("The setting " + sKey + " has been saved.");
                }
                catch (Exception ex)
                {
                    if (lh != null)
                    {
                        lh.LogError(ex, MethodBase.GetCurrentMethod());
                        /*
                        lh.LogError("Error in Save Settings : " + ex.ToString());
                        if (ex.InnerException != null)
                            lh.LogError(ex.InnerException.ToString());
                        */
                    }
                }
        }

        public void SaveOutputFolderToConfigFile()
        {
            SaveSetting(APP_Config_Keys.output, this.output, lh);

        }

        public void SaveAsciiFolderToConfigFile()
        {
            SaveSetting(APP_Config_Keys.gridAsciiFolder, this.GridAsciiFolder, lh);

        }

        public void SaveSoundGridStepToConfigFile()
        {
            SaveSetting(APP_Config_Keys.gridStep, this.dGridStep, lh);

        }

        public void SaveSoundGridBinaryFieToConfigFile()
        {
            SaveSetting(APP_Config_Keys.gridBinaryFile, this.GridBinary, lh);

        }
        public void Save3MBMovemetFileToConfigFile()
        {
            SaveSetting(APP_Config_Keys._3mbIniFile, this._3mbIniFile, lh);

        }

        //     public static string soundSourceType = "sourceType";
        // public static string soundMetric = "soundMetric";
        public void SaveSoundSourceTypeToConfigFile()
        {
            SaveSetting(APP_Config_Keys.soundSourceType, this.soundSourceType, lh);

        }
    }
}
