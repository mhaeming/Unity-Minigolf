using System.Collections.Generic;

namespace Code.Experiment
{
    public class Statistics
    {
        public int trialNr;
        public bool isDecision;
        public float time;
        public int items;
        public int interactions;
        public float metres;
        public int failures;
        public int levels;
        public int answer1;
        public int answer2;
        public int answer3;
        public bool csvUpdated = false;
        public bool dataCollected = false;
        public List<string> csvHeader = new List<string>();
        public List<List<string>> csvData = new List<List<string>>();
    }
}