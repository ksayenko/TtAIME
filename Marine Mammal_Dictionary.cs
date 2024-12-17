using Microsoft.Win32;
using Read3mb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetratechTools;

namespace RunManatea
{
    public class Marine_Mammal_Dictionary
    {
        List<Marine_Species_Threshhold> defaultList;
        List<StandardAcousticThreshold> headers;
        AppSettings appSettings;
        FileInfo default_file;
        LogHandler lh;

        public List<Marine_Species_Threshhold> DefaultList { get => defaultList; set => defaultList = value; }
        public List<StandardAcousticThreshold> Headers { get => headers; set => headers = value; }

        public Marine_Mammal_Dictionary(AppSettings appSettings, LogHandler lh)
        {
            this.appSettings = appSettings;
            this.lh = lh;
            if (appSettings == null) { appSettings = new AppSettings(); }
            if (lh == null) { lh = new LogHandler(); }

            PopulateStandardAcousticThreshhold();
            PopulateDefaultLIst();

        }

        public List<Marine_Species> SortByOrderHearingGroupName(List<Marine_Species> list)
        {
            List < Marine_Species > newList = new List<Marine_Species>();

            List<Marine_Species> list1 = list.Distinct().ToList();

            newList= list1.OrderBy(m => m.OrderInTheList).ThenBy(m => m.HearingGroup.Name).ThenBy(m => m.Scientific_name).ToList();


            return newList;
        }

        private void PopulateStandardAcousticThreshhold()
        {
            headers = new List<StandardAcousticThreshold>();
            Acoustic_Threshold as_AUD1 = new Acoustic_Threshold("Auditory Injury (AUD INJ)",
                "AUD INJ",
                "Auditory Injury (AUD INJ)", SoundSourceType.Impulsive, SoundSourceMetric.SEL_Weighted, "db", 0);
            Acoustic_Threshold as_AUD2 = as_AUD1.Copy();
            as_AUD2.TheSoundSourceType = SoundSourceType.Non_Impulsive;
            Acoustic_Threshold as_AUD3 = as_AUD1.Copy();
            as_AUD3.TheSoundSourceMetric = SoundSourceMetric.PK_Unweighted;

            headers.Add(new StandardAcousticThreshold(as_AUD1, 0));
            headers.Add(new StandardAcousticThreshold(as_AUD2, 1));
            headers.Add(new StandardAcousticThreshold(as_AUD3, 2));


            Acoustic_Threshold as_TTS1 = new Acoustic_Threshold("Temporary Threshold Shift (TTS)",
               "TTS",
               "Temporary Threshold Shift (TTS)", SoundSourceType.Impulsive, SoundSourceMetric.SEL_Weighted, "db", 0);
            Acoustic_Threshold as_TTS2 = as_TTS1.Copy();
            as_TTS2.TheSoundSourceType = SoundSourceType.Non_Impulsive;
            Acoustic_Threshold as_TTS3 = as_TTS1.Copy();
            as_TTS3.TheSoundSourceMetric = SoundSourceMetric.PK_Unweighted;

            headers.Add(new StandardAcousticThreshold(as_TTS1, 3));
            headers.Add(new StandardAcousticThreshold(as_TTS2, 4));
            headers.Add(new StandardAcousticThreshold(as_TTS3, 5));

            //7 and 8
            //Behavioral Disturbance	Behavioral Disturbance
            //SPL(Unweighted)    SPL(Unweighted)
            //Impulsive          Non-Impulsive
            Acoustic_Threshold as_Beh1 = new Acoustic_Threshold("Behavioral Disturbance",
                "Behavioral Disturbance",
                "Behavioral Disturbance",
                SoundSourceType.Impulsive, SoundSourceMetric.SPL_Unweighted, "db", 0);

            Console.WriteLine(as_Beh1.ToString());

            Acoustic_Threshold as_Beh2 = as_Beh1.Copy();
            as_Beh2.TheSoundSourceType = SoundSourceType.Non_Impulsive;
            as_Beh2.TheSoundSourceMetric = SoundSourceMetric.SPL_Unweighted;

            headers.Add(new StandardAcousticThreshold(as_Beh1, 6));
            headers.Add(new StandardAcousticThreshold(as_Beh2, 7));
       
            //

            Acoustic_Threshold as_Prob1 = new Acoustic_Threshold("Probabilistic Behavioral Disturbance",
                "Probabilistic Behavioral Disturbance", "Probabilistic Behavioral Disturbance 1",
                SoundSourceType.Impulsive, SoundSourceMetric.SPL_Weighted, "db", 0);

            Acoustic_Threshold as_Prob2 = as_Prob1.Copy();
            as_Prob2.Description = "Probabilistic Behavioral Disturbance 2";
            Acoustic_Threshold as_Prob3 = as_Prob1.Copy();
            as_Prob3.Description = "Probabilistic Behavioral Disturbance 3";

            Acoustic_Threshold as_Prob4 = as_Prob1.Copy();
            as_Prob4.TheSoundSourceType = SoundSourceType.Non_Impulsive;
            as_Prob4.Description = "Probabilistic Behavioral Disturbance 1";
            Acoustic_Threshold as_Prob5 = as_Prob4.Copy();
            as_Prob5.Description = "Probabilistic Behavioral Disturbance 2";

            Acoustic_Threshold as_Prob6 = as_Prob4.Copy();
            as_Prob6.Description = "Probabilistic Behavioral Disturbance 3";

            headers.Add(new StandardAcousticThreshold(as_Prob1, 8));
            headers.Add(new StandardAcousticThreshold(as_Prob2, 9));
            headers.Add(new StandardAcousticThreshold(as_Prob3, 10));

            headers.Add(new StandardAcousticThreshold(as_Prob4, 11));
            headers.Add(new StandardAcousticThreshold(as_Prob5, 12));
            headers.Add(new StandardAcousticThreshold(as_Prob6, 13));
            headers.Sort();

        }

        private Acoustic_Threshold Get_Acoustic_Threshold_from_List(string shortname, SoundSourceMetric metric, SoundSourceType sourceType)
        {
            Acoustic_Threshold as1 = new Acoustic_Threshold(shortname, shortname, shortname, sourceType, metric, "db", 0);
            if (headers.Count == 0)
                return as1;

            foreach (StandardAcousticThreshold header in headers)
            {
                if (header.Threshhold.Name == shortname &&
                    header.Threshhold.TheSoundSourceMetric == metric &&
                    header.Threshhold.TheSoundSourceType == sourceType)
                    return header.Threshhold;
            }

            return as1;
        }

        private Acoustic_Threshold Get_Acoustic_Threshold_from_List(int order)
        {
            Acoustic_Threshold as1 = new Acoustic_Threshold();
            if (headers.Count == 0)
                return as1;

            foreach (StandardAcousticThreshold header in headers)
            {
                if (header.Order == order)
                    return header.Threshhold;
            }

            return as1;
        }


        public List<Marine_Species> DistinctSpecies()
        {
            List<Marine_Species> distSpec = new List<Marine_Species>();
            foreach (Marine_Species_Threshhold m in defaultList)
            {
                Marine_Species ms = m.Marine_Species;

                bool bExists = false;
                foreach (Marine_Species dist in distSpec)
                {
                    if (dist.CompareTo(ms) == 0)
                    {
                        bExists = true;
                        break;
                    }
                }

                if (!bExists)
                    distSpec.Add(ms);
            }

            return distSpec;
        }
        private void PopulateDefaultLIst()
        {
            if (defaultList == null)
                defaultList = new List<Marine_Species_Threshhold>();
            defaultList.Clear();

            //standard Acoustic threshholds
            Acoustic_Threshold aud1 = Get_Acoustic_Threshold_from_List(0);
            Acoustic_Threshold aud2 = Get_Acoustic_Threshold_from_List(1);
            Acoustic_Threshold aud3 = Get_Acoustic_Threshold_from_List(2);

            Acoustic_Threshold tts1 = Get_Acoustic_Threshold_from_List(3);
            Acoustic_Threshold tts2 = Get_Acoustic_Threshold_from_List(4);
            Acoustic_Threshold tts3 = Get_Acoustic_Threshold_from_List(5);


            Acoustic_Threshold beh1 = Get_Acoustic_Threshold_from_List(6);
            Acoustic_Threshold beh2 = Get_Acoustic_Threshold_from_List(7);

            Acoustic_Threshold probBeh1 = Get_Acoustic_Threshold_from_List(8);
            Acoustic_Threshold probBeh2 = Get_Acoustic_Threshold_from_List(9);
            Acoustic_Threshold probBeh3 = Get_Acoustic_Threshold_from_List(10);
            Acoustic_Threshold probBeh4 = Get_Acoustic_Threshold_from_List(11);
            Acoustic_Threshold probBeh5 = Get_Acoustic_Threshold_from_List(12);
            Acoustic_Threshold probBeh6 = Get_Acoustic_Threshold_from_List(13);



            // By Marine SpeciesM
            int i = 0;
            Marine_Species mNorthAtlanticRightWhale = new Marine_Species("North Atlantic Right Whale", "Eubalaena glacialis", "LF Cetacean");
            mNorthAtlanticRightWhale.OrderInTheList = i++; ;
            
            Marine_Species mFinWhale = new Marine_Species("Fin Whale", "Balaenoptera physalus", "LF Cetacean");
            mFinWhale.OrderInTheList = i++; 

            Marine_Species mBlueWhale = new Marine_Species("Blue Whale", "Balaenoptera musculus", "LF Cetacean");
            mBlueWhale.OrderInTheList = i++;

            Marine_Species mMinkeWhale = new Marine_Species("Minke Whale", "Balaenoptera acutorostrata ", "LF Cetacean");
            mMinkeWhale.OrderInTheList = i++;

            Marine_Species mSeiWhale = new Marine_Species("Sei Whale", "Balaenoptera borealis", "LF Cetacean");
            mSeiWhale.OrderInTheList = i++; ;

            Marine_Species mHumpbackWhale = new Marine_Species("Humpback Whale", "Megaptera novaeangliae", "LF Cetacean");
            mHumpbackWhale.OrderInTheList = i++; ;

            Marine_Species mRicesWhale = new Marine_Species("Rice’s Whale", "Balaenoptera ricei", "LF Cetacean", i++);
            Marine_Species mEdensWhale = new Marine_Species("Eden’s Whale", "Balaenoptera edeni", "LF Cetacean", i++); ;
            Marine_Species mOmurasWhale = new Marine_Species("Omura’s Whale", "Balaenoptera omurai", "LF Cetacean", i++);
            Marine_Species mGrayWhale = new Marine_Species("Gray Whale", "Eschrichtius robustus", "LF Cetacean", i++);

            Marine_Species mBottlenosedolphin = new Marine_Species("Bottlenose dolphin", "Tursiops truncatus", "HF Cetacean", i++);
            Marine_Species mPantropicalspotteddolphin = new Marine_Species("Pantropical spotted dolphin", "Stenella attenuata",
                "HF Cetacean", i++);
            Marine_Species mShortbeakedcommondolphin = new Marine_Species("Short-beaked common dolphin", "Delphinus delphis",
                "HF Cetacean", i++);
            Marine_Species mPilotwhale = new Marine_Species("Pilot whale", "Globicephala sp.", "HF Cetacean", i++);
            Marine_Species mSpermwhale = new Marine_Species("Sperm whale", "Physeter macrocephalus", "HF Cetacean", i++);
            Marine_Species mRissosdolphin = new Marine_Species("Risso’s dolphin", "Grampus griseus", "HF Cetacean", i++); ;
            Marine_Species mAtlanticspotteddolphin = new Marine_Species("Atlantic spotted dolphin", "Stenella frontalis", 
                "HF Cetacean", i++);

            Marine_Species mHarborporpoise = new Marine_Species("Harbor porpoise", "Phocoena phocoena", "VHF Cetacean", i++);

            Marine_Species mGreyseal = new Marine_Species("Grey seal", "Halichoerus grypus", "PW", i++);
            Marine_Species mHarborseal = new Marine_Species("Harbor seal", "Phoca vitulina", "PW", i++);

            Marine_Species mGreenturtle = new Marine_Species("Green turtle", "Chelonia mydas", "TU", i++);
            Marine_Species mKempsridleyturtle = new Marine_Species("Kemp’s ridley turtle", "Lepidochelys kempii", "TU", i++);
            Marine_Species mLoggerheadturtle = new Marine_Species("Loggerhead turtle", "Caretta caretta", "TU", i++); ;
            Marine_Species mLeatherbackturtle = new Marine_Species("Leatherback turtle", "Dermochelys coriacea", "TU", i++);


            #region LF definition

            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mNorthAtlanticRightWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));

            //---
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mFinWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));

            //---

            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mBlueWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));

            //----


            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mMinkeWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));

            //----


            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mSeiWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));
            //--

            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mHumpbackWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));
            ///

            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));


            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mRicesWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));

            //--

            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));


            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mEdensWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));


            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mOmurasWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));


            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), aud2.CopyAndUpdateThreshold(197)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), aud3.CopyAndUpdateThreshold(222)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), tts2.CopyAndUpdateThreshold(177)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), tts3.CopyAndUpdateThreshold(216)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh3.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mGrayWhale.Copy(), probBeh6.CopyAndUpdateThreshold(160)));
            #endregion

            #region HF definition
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mBottlenosedolphin.Copy(), probBeh6.CopyAndUpdateThreshold(180)));

            //
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPantropicalspotteddolphin.Copy(), probBeh6.CopyAndUpdateThreshold(180)));
           
            
            //
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mShortbeakedcommondolphin.Copy(), probBeh6.CopyAndUpdateThreshold(180)));
            //
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mPilotwhale.Copy(), probBeh6.CopyAndUpdateThreshold(180)));

            //
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), tts3.CopyAndUpdateThreshold(224)));

            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mSpermwhale.Copy(), probBeh6.CopyAndUpdateThreshold(180)));
            //mPilotwhale
            //

            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mRissosdolphin.Copy(), probBeh6.CopyAndUpdateThreshold(180)));

            //
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), aud1.CopyAndUpdateThreshold(193)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), aud2.CopyAndUpdateThreshold(201)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), aud3.CopyAndUpdateThreshold(230)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), tts1.CopyAndUpdateThreshold(178)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), tts2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), tts3.CopyAndUpdateThreshold(224)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mAtlanticspotteddolphin.Copy(), probBeh6.CopyAndUpdateThreshold(180)));

            #endregion

            #region vhf definition
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), aud1.CopyAndUpdateThreshold(159)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), aud2.CopyAndUpdateThreshold(181)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), aud3.CopyAndUpdateThreshold(202)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), tts1.CopyAndUpdateThreshold(144)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), tts2.CopyAndUpdateThreshold(161)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), tts3.CopyAndUpdateThreshold(196)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh1.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh2.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh4.CopyAndUpdateThreshold(120)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh5.CopyAndUpdateThreshold(140)));
            //defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh5.CopyAndUpdateThreshold(120)));
            //defaultList.Add(new Marine_Species_Threshhold(mHarborporpoise.Copy(), probBeh6.CopyAndUpdateThreshold(140)));


            #endregion

            #region PW definition
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), aud2.CopyAndUpdateThreshold(195)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), aud3.CopyAndUpdateThreshold(223)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), tts2.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), tts3.CopyAndUpdateThreshold(217)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mGreyseal.Copy(), probBeh6.CopyAndUpdateThreshold(180)));


            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), aud1.CopyAndUpdateThreshold(183)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), aud2.CopyAndUpdateThreshold(195)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), aud3.CopyAndUpdateThreshold(223)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), tts1.CopyAndUpdateThreshold(168)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), tts2.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), tts3.CopyAndUpdateThreshold(217)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), beh1.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), beh2.CopyAndUpdateThreshold(120)));

            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh1.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh2.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh3.CopyAndUpdateThreshold(180)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh4.CopyAndUpdateThreshold(140)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh5.CopyAndUpdateThreshold(160)));
            defaultList.Add(new Marine_Species_Threshhold(mHarborseal.Copy(), probBeh6.CopyAndUpdateThreshold(180)));


            #endregion
            #region TU  definition

            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), aud1.CopyAndUpdateThreshold(204)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), aud2.CopyAndUpdateThreshold(220)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), aud3.CopyAndUpdateThreshold(232)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), tts1.CopyAndUpdateThreshold(189)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), tts2.CopyAndUpdateThreshold(200)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), tts3.CopyAndUpdateThreshold(226)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), beh1.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), beh2.CopyAndUpdateThreshold(175)));
            
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh1.CopyAndUpdateThreshold(double.NaN)));
            //defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh2.CopyAndUpdateThreshold(double.NaN)));
            //defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh3.CopyAndUpdateThreshold(double.NaN)));
            defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh4.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh5.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mGreenturtle.Copy(), probBeh6.CopyAndUpdateThreshold(double.NaN)));


            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), aud1.CopyAndUpdateThreshold(204)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), aud2.CopyAndUpdateThreshold(220)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), aud3.CopyAndUpdateThreshold(232)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), tts1.CopyAndUpdateThreshold(189)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), tts2.CopyAndUpdateThreshold(200)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), tts3.CopyAndUpdateThreshold(226)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), beh1.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), beh2.CopyAndUpdateThreshold(175)));

            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh1.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh2.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh3.CopyAndUpdateThreshold(double.NaN)));
            defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh4.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh5.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mKempsridleyturtle.Copy(), probBeh6.CopyAndUpdateThreshold(double.NaN)));


            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), aud1.CopyAndUpdateThreshold(204)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), aud2.CopyAndUpdateThreshold(220)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), aud3.CopyAndUpdateThreshold(232)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), tts1.CopyAndUpdateThreshold(189)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), tts2.CopyAndUpdateThreshold(200)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), tts3.CopyAndUpdateThreshold(226)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), beh1.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), beh2.CopyAndUpdateThreshold(175)));


            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh1.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh2.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh3.CopyAndUpdateThreshold(double.NaN)));
            defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh4.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh5.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mLoggerheadturtle.Copy(), probBeh6.CopyAndUpdateThreshold(double.NaN)));


            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), aud1.CopyAndUpdateThreshold(204)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), aud2.CopyAndUpdateThreshold(220)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), aud3.CopyAndUpdateThreshold(232)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), tts1.CopyAndUpdateThreshold(189)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), tts2.CopyAndUpdateThreshold(200)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), tts3.CopyAndUpdateThreshold(226)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), beh1.CopyAndUpdateThreshold(175)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), beh2.CopyAndUpdateThreshold(175)));

            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh1.CopyAndUpdateThreshold(double.NaN)));
            //defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh2.CopyAndUpdateThreshold(double.NaN)));
           // defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh3.CopyAndUpdateThreshold(double.NaN)));
            defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh4.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh5.CopyAndUpdateThreshold(double.NaN)));
          //  defaultList.Add(new Marine_Species_Threshhold(mLeatherbackturtle.Copy(), probBeh6.CopyAndUpdateThreshold(double.NaN)));
            #endregion

            /*
             * Marine Species 	Scientific Name 	Hearing Group/ 	Acoustic Thresholds (dB) 								
		Auditory Weighting Function 	Auditory Injury (AUD INJ) 			Temporary Threshold Shift (TTS) 			Behavioral Disturbance 		
			SEL (Weighted) 		PK (Unweighted) 	SEL (Weighted) 		PK (Unweighted) 	SPL (Unweighted) 		SPL (Weighted) 
			I 	NI 	I 	I 	NI 	I 	I 	NI 	I 

North Atlantic Right Whale 	Eubalaena glacialis 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Fin Whale 	Balaenoptera physalus 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Blue Whale 	Balaenoptera musculus 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Minke Whale 	Balaenoptera acutorostrata 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Sei Whale 	Balaenoptera borealis 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Humpback Whale 	Megaptera novaeangliae 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Rice’s Whale 	Balaenoptera ricei 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Eden’s Whale 	Balaenoptera edeni 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Omura’s Whale 	Balaenoptera omurai 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Gray Whale 	Eschrichtius robustus 	LF Cetacean 	183 	197 	222 	168 	177 	216 	160 	120 	
Bottlenose dolphin 	Tursiops truncatus 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Pantropical spotted dolphin 	Stenella attenuata 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Short-beaked common dolphin 	Delphinus delphis 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Pilot whale 	Globicephala sp. 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Sperm whale 	Physeter macrocephalus 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Risso’s dolphin 	Grampus griseus 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Atlantic spotted dolphin 	Stenella frontalis 	HF Cetacean 	193 	201 	230 	178 	181 	224 	160 	120 	
Harbor porpoise 	Phocoena phocoena 	VHF Cetacean 	159 	181 	202 	144 	161 	196 	160 	120 	
Grey seal 	Halichoerus grypus 	PW 	183 	195 	223 	168 	175 	217 	160 	120 	
Harbor seal 	Phoca vitulina 	PW 	183 	195 	223 	168 	175 	217 	160 	120 	
Green turtle 	Chelonia mydas 	TU 	204 	220 	232 	189 	200 	226 	175 		Not applicable 
Kemp’s ridley turtle 	Lepidochelys kempii 	TU 	204 	220 	232 	189 	200 	226 	175 		Not applicable 
Loggerhead turtle 	Caretta caretta 	TU 	204 	220 	232 	189 	200 	226 	175 		Not applicable 
Leatherback turtle 	Dermochelys coriacea 	TU 	204 	220 	232 	189 	200 	226 	175 		Not applicable 
            */



        }
        private bool Read()
        {
            throw new NotImplementedException();
        }

        private bool Write(string path)
        {
            appSettings.SaveSetting(APP_Config_Keys.TtAIME_Marine_Mammal_Dictionary_file, path, lh);
            throw new NotImplementedException();
        }

    }
}
