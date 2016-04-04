using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
    public class CoastalRaidersFuedalResourceManager
    {
        //input
        private int _sn_food_gatherer_percentage_fa;
        private int _sn_wood_gatherer_percentage_fa;
        private int _sn_gold_gatherer_percentage_fa;
        private int _sn_stone_gatherer_percentage_fa;
        private int _sn_percent_civilian_builders_fa;

        //output
        private int _food_Score;
        private int _wood_Score;
        private int _stone_Score;
        private int _gold_Score;

        /// <summary>
        /// Returns an array of input
        /// </summary>
        public double[] GetInputParams
        {
            get
            {
                return
                     new double[]
                     {
                         (double)_sn_food_gatherer_percentage_fa,
                         (double)_sn_wood_gatherer_percentage_fa,
                         (double)_sn_gold_gatherer_percentage_fa,
                         (double)_sn_stone_gatherer_percentage_fa,
                         (double)_sn_percent_civilian_builders_fa
                     };
            }
        }

        /// <summary>
        /// Returns an array of outputs.
        /// </summary>
        public double[] GetOutputParams
        {
            get
            {
                return new double[]
                {
                    (double)_food_Score,
                    (double)_wood_Score,
                    (double)_stone_Score,
                    (double)_gold_Score
                };
            }
        }

        /// <summary>
        /// Constructor for the ai training set.
        /// </summary>
        /// <param name="sn_food_gatherer_percentage_fa"></param>
        /// <param name="sn_wood_gatherer_percentage_fa"></param>
        /// <param name="sn_gold_gatherer_percentage_fa"></param>
        /// <param name="sn_stone_gatherer_percentage_fa"></param>
        /// <param name="sn_percent_civilian_builders_fa"></param>
        public CoastalRaidersFuedalResourceManager(
            int sn_food_gatherer_percentage_fa,
			int sn_wood_gatherer_percentage_fa,
			int sn_gold_gatherer_percentage_fa,
			int sn_stone_gatherer_percentage_fa,
			int sn_percent_civilian_builders_fa,
            int food_Score,
            int wood_Score,
            int stone_Score,
            int gold_Score)
        {
            _sn_food_gatherer_percentage_fa = sn_food_gatherer_percentage_fa;
            _sn_wood_gatherer_percentage_fa = sn_wood_gatherer_percentage_fa;
            _sn_gold_gatherer_percentage_fa = sn_gold_gatherer_percentage_fa;
            _sn_stone_gatherer_percentage_fa = sn_stone_gatherer_percentage_fa;
            _sn_percent_civilian_builders_fa = sn_percent_civilian_builders_fa;

            _food_Score = food_Score;
            _wood_Score = wood_Score;
            _stone_Score = stone_Score;
            _gold_Score = gold_Score;
        }  
    }      
}          
           