﻿-- =============================================
-- Author:		Nish
-- Create date: 01/14/2021
-- Description:	Save CME 
-- =============================================
CREATE PROCEDURE exch.save_cme_future_position_limit
	@run_id bigint,
	@create_date varchar(1024),
	@create_user varchar(1024),
	@last_update_date varchar(1024),
	@last_update_user varchar(1024),
	@contract_name varchar(1024),
	@rule_chapter varchar(1024),
	@commodity_code varchar(1024),
	@contract_size varchar(1024),
	@contract_units varchar(1024),
	@contract_type varchar(1024),
	@settlement varchar(1024),
	@contract_group varchar(1024),
	@diminishing_balance_contract varchar(1024),
	@reporting_level varchar(1024),
	@spot_month_position_comprised_of_futures_and_deliveries varchar(1024),
	@spot_month_aggregate_into_futures_equivalent_leg_1 varchar(1024),
	@spot_month_aggregate_into_futures_equivalent_leg_2 varchar(1024),
	@spot_month_aggregate_into_ratio_leg_1 varchar(1024),
	@spot_month_aggregate_into_ratio_leg_2 varchar(1024),
	@spot_Month_Accountability_Level varchar(1024),
	@daily_accountability_level_for_daily_contract varchar(1024),
	@initial_spot_month_limit_in_net_futures_equivalents_leg_1_over_leg_2 varchar(1024),
	@initial_spot_month_limit_effective_date varchar(1024),
	@spot_month_limit_in_contract_units_leg_1_over_leg_2 varchar(1024),
	@subsequent_spot_month_limits_in_net_futures_equivalents varchar(1024),
	@subsequent_spot_month_limits_effective_dates varchar(1024),
	@single_month_aggregate_into_futures_equivalent_leg_1 varchar(1024),
	@single_month_aggregate_into_futures_equivalent_leg_2 varchar(1024),
	@single_month_aggregate_into_ratio_leg_1 varchar(1024),
	@single_month_aggregate_into_ratio_leg_2 varchar(1024),
	@single_month_accountability_level_leg_1_over_leg_2 varchar(1024),
	@single_month_limit_in_net_futures_equivalents_leg_1_over_leg_2 varchar(1024),
	@all_month_aggregate_into_futures_equivalent_leg_1 varchar(1024),
	@all_month_aggregate_into_futures_equivalent_leg_2 varchar(1024),
	@all_month_aggregate_into_ratio_leg_1 varchar(1024),
	@all_month_aggregate_into_ratio_leg_2 varchar(1024),
	@all_month_accountability_level_leg_1_over_leg_2 varchar(1024),
	@all_month_limit_in_net_futures_equivalents_leg_1_over_leg_2  varchar(1024)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO exch.cme_future_position_limit(
		run_id,
		create_date,
		create_user,
		last_update_date,
		last_update_user,
		contract_name,
		rule_chapter,
		commodity_code,
		contract_size,
		contract_units,
		contract_type,
		settlement,
		contract_group,
		diminishing_balance_contract,
		reporting_level,
		spot_month_position_comprised_of_futures_and_deliveries,
		spot_month_aggregate_into_futures_equivalent_leg_1,
		spot_month_aggregate_into_futures_equivalent_leg_2,
		spot_month_aggregate_into_ratio_leg_1,
		spot_month_aggregate_into_ratio_leg_2,
		spot_Month_Accountability_Level,
		daily_accountability_level_for_daily_contract,
		initial_spot_month_limit_in_net_futures_equivalents_leg_1_over_leg_2,
		initial_spot_month_limit_effective_date,
		spot_month_limit_in_contract_units_leg_1_over_leg_2,
		subsequent_spot_month_limits_in_net_futures_equivalents,
		subsequent_spot_month_limits_effective_dates,
		single_month_aggregate_into_futures_equivalent_leg_1,
		single_month_aggregate_into_futures_equivalent_leg_2,
		single_month_aggregate_into_ratio_leg_1,
		single_month_aggregate_into_ratio_leg_2,
		single_month_accountability_level_leg_1_over_leg_2,
		single_month_limit_in_net_futures_equivalents_leg_1_over_leg_2,
		all_month_aggregate_into_futures_equivalent_leg_1,
		all_month_aggregate_into_futures_equivalent_leg_2,
		all_month_aggregate_into_ratio_leg_1,
		all_month_aggregate_into_ratio_leg_2,
		all_month_accountability_level_leg_1_over_leg_2,
		all_month_limit_in_net_futures_equivalents_leg_1_over_leg_2
	)
	values ( 
		@run_id,
		@create_date,
		@create_user,
		@last_update_date,
		@last_update_user,
		@contract_name,
		@rule_chapter,
		@commodity_code,
		@contract_size,
		@contract_units,
		@contract_type,
		@settlement,
		@contract_group,
		@diminishing_balance_contract,
		@reporting_level,
		@spot_month_position_comprised_of_futures_and_deliveries,
		@spot_month_aggregate_into_futures_equivalent_leg_1,
		@spot_month_aggregate_into_futures_equivalent_leg_2,
		@spot_month_aggregate_into_ratio_leg_1,
		@spot_month_aggregate_into_ratio_leg_2,
		@spot_Month_Accountability_Level,
		@daily_accountability_level_for_daily_contract,
		@initial_spot_month_limit_in_net_futures_equivalents_leg_1_over_leg_2,
		@initial_spot_month_limit_effective_date,
		@spot_month_limit_in_contract_units_leg_1_over_leg_2,
		@subsequent_spot_month_limits_in_net_futures_equivalents,
		@subsequent_spot_month_limits_effective_dates,
		@single_month_aggregate_into_futures_equivalent_leg_1,
		@single_month_aggregate_into_futures_equivalent_leg_2,
		@single_month_aggregate_into_ratio_leg_1,
		@single_month_aggregate_into_ratio_leg_2,
		@single_month_accountability_level_leg_1_over_leg_2,
		@single_month_limit_in_net_futures_equivalents_leg_1_over_leg_2,
		@all_month_aggregate_into_futures_equivalent_leg_1,
		@all_month_aggregate_into_futures_equivalent_leg_2,
		@all_month_aggregate_into_ratio_leg_1,
		@all_month_aggregate_into_ratio_leg_2,
		@all_month_accountability_level_leg_1_over_leg_2,
		@all_month_limit_in_net_futures_equivalents_leg_1_over_leg_2
	)
END