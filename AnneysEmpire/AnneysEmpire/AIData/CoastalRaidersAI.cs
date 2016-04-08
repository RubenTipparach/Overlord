using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnneysEmpire
{
	public class CoastalRaidersAI
	{
		// Drop distance.
		public int sn_food_dropsite_distance = 5;
		public int sn_wood_dropsite_distance = 6;
		public int sn_gold_dropsite_distance = 5;
		public int sn_stone_dropsite_distance = 5;

		// The following is random group sizes for parties.
		//	public int  sn_special_attack_type1 villager
		public int sn_mill_max_distance = 45;
		public int sn_do_not_scale_for_difficulty_level = 1;

		public int sn_number_boat_explore_groups = 1;
		public int sn_minimum_boat_explore_group_size = 1;
		public int sn_maximum_boat_explore_group_size = 1;
		public int sn_number_boat_defend_groups = 1;
		public int sn_maximum_boat_defend_group_size = 6;
		public int sn_number_boat_attack_groups = 3;
		public int sn_minimum_boat_attack_group_size = 3;

		public int sn_cap_civilian_builders = 4;
		public int sn_cap_civilian_explorers = 0;
		public int sn_number_explore_groups = 1;
		public int sn_minimum_explore_group_size = 1;
		public int sn_maximum_explore_group_size = 1;

		//dark age population cimb <= 10
		public int sn_food_gatherer_percentage_da1 = 75;
		public int sn_wood_gatherer_percentage_da1 = 25;
		public int sn_percent_civilian_builders_da1 = 10;
		// > 10
		public int sn_food_gatherer_percentage_da2 = 62;
		public int sn_wood_gatherer_percentage_da2 = 33;
		public int sn_gold_gatherer_percentage_da2 = 5;
		public int sn_percent_civilian_builders_da2 = 5;
		// > 15
		public int sn_food_gatherer_percentage_da3 = 55;
		public int sn_wood_gatherer_percentage_da3 = 40;

		// Feudal age strategic numbers.
		public int sn_food_gatherer_percentage_fa = 47;
		public int sn_wood_gatherer_percentage_fa = 41;
		public int sn_gold_gatherer_percentage_fa = 12;
		public int sn_stone_gatherer_percentage_fa = 5;
		public int sn_percent_civilian_builders_fa = 10;

		// Castle age strategic numbers.
		public int sn_food_gatherer_percentage_ca = 30;
		public int sn_wood_gatherer_percentage_ca = 32;
		public int sn_gold_gatherer_percentage_ca = 33;
		public int sn_stone_gatherer_percentage_ca = 6;
		public int sn_number_forward_builders_ca = 4;
		public int sn_maximum_town_size_ca = 100;

		// Imperial age strategic numbers.
		public int sn_food_gatherer_percentage_ia = 30;
		public int sn_wood_gatherer_percentage_ia = 25;
		public int sn_gold_gatherer_percentage_ia = 35;
		public int sn_stone_gatherer_percentage_ia = 10;
		public int sn_maximum_town_size_ia = 150;

		double[] resourceOnlyAiInputs;
		double[] partialAiInputs;
		double[] fullAiInputs;

		public CoastalRaidersAI()
		{
			resourceOnlyAiInputs = new double[]
			{
				//fuedal age.
				sn_food_gatherer_percentage_fa/100.0,
				sn_wood_gatherer_percentage_fa/100.0,
				sn_gold_gatherer_percentage_fa/100.0,
				sn_stone_gatherer_percentage_fa/100.0,
				sn_percent_civilian_builders_fa/100.0,
			};

			partialAiInputs = new double[]
			{
				//drop distance
				sn_food_dropsite_distance,  	
				sn_wood_dropsite_distance, 	
				sn_gold_dropsite_distance, 	
				sn_stone_dropsite_distance,
 				sn_mill_max_distance ,				
				sn_do_not_scale_for_difficulty_level,
				//random numbers
				sn_number_boat_explore_groups, 			
				sn_minimum_boat_explore_group_size, 		
				sn_maximum_boat_explore_group_size, 		
				sn_number_boat_defend_groups, 			
				sn_maximum_boat_defend_group_size, 		
				sn_number_boat_attack_groups, 			
				sn_minimum_boat_attack_group_size,
 				sn_cap_civilian_builders,
				sn_cap_civilian_explorers,	
				sn_number_explore_groups,	
				sn_minimum_explore_group_size, 
				sn_maximum_explore_group_size,

				//dark ages stuff
				sn_food_gatherer_percentage_da1/100.0, 
				sn_wood_gatherer_percentage_da1/100.0,
				sn_percent_civilian_builders_da1/100.0,
  
				sn_food_gatherer_percentage_da2/100.0,
				sn_wood_gatherer_percentage_da2/100.0,
				sn_gold_gatherer_percentage_da2/100.0,
				sn_percent_civilian_builders_da2/100.0,

				sn_food_gatherer_percentage_da3/100.0,
				sn_wood_gatherer_percentage_da3/100.0,

				//fuedal age.
				sn_food_gatherer_percentage_fa/100.0,
				sn_wood_gatherer_percentage_fa/100.0,
				sn_gold_gatherer_percentage_fa/100.0,
				sn_stone_gatherer_percentage_fa/100.0,
				sn_percent_civilian_builders_fa/100.0,
			};

			double[] fullAiInputs = new double[]
			{
				//drop distance
				sn_food_dropsite_distance,  	
				sn_wood_dropsite_distance, 	
				sn_gold_dropsite_distance, 	
				sn_stone_dropsite_distance,
 				sn_mill_max_distance ,				
				sn_do_not_scale_for_difficulty_level,
				//random numbers
				sn_number_boat_explore_groups, 			
				sn_minimum_boat_explore_group_size, 		
				sn_maximum_boat_explore_group_size, 		
				sn_number_boat_defend_groups, 			
				sn_maximum_boat_defend_group_size, 		
				sn_number_boat_attack_groups, 			
				sn_minimum_boat_attack_group_size,
 				sn_cap_civilian_builders,
				sn_cap_civilian_explorers,	
				sn_number_explore_groups,	
				sn_minimum_explore_group_size, 
				sn_maximum_explore_group_size,

				//dark ages stuff
				sn_food_gatherer_percentage_da1, 
				sn_wood_gatherer_percentage_da1 ,
				sn_percent_civilian_builders_da1,
  
				sn_food_gatherer_percentage_da2 ,
				sn_wood_gatherer_percentage_da2 ,
				sn_gold_gatherer_percentage_da2 ,
				sn_percent_civilian_builders_da2,

				sn_food_gatherer_percentage_da3,
				sn_wood_gatherer_percentage_da3,

				//fuedal age.
				sn_food_gatherer_percentage_fa 	,
				sn_wood_gatherer_percentage_fa 	,
				sn_gold_gatherer_percentage_fa 	,
				sn_stone_gatherer_percentage_fa ,
				sn_percent_civilian_builders_fa	,

				sn_food_gatherer_percentage_ca	,
				sn_wood_gatherer_percentage_ca 	,
				sn_gold_gatherer_percentage_ca 	,
				sn_stone_gatherer_percentage_ca ,
				sn_number_forward_builders_ca 	,
				sn_maximum_town_size_ca 		,

				sn_food_gatherer_percentage_ia ,	
				sn_wood_gatherer_percentage_ia	,
				sn_gold_gatherer_percentage_ia	,
				sn_stone_gatherer_percentage_ia,	
				sn_maximum_town_size_ia	
			};
		}


	}
}
