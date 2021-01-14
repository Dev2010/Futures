-- =============================================
-- Author:		Nish
-- Create date: 01/14/2021
-- Description:	Save CME 
-- =============================================
CREATE PROCEDURE exch.save_cme_future_position_limit
	@cme_future_positionlimit exch.cme_future_position_limit_table_type READONLY
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO exch.cme_future_position_limit(
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
	SELECT 
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
	from @cme_future_positionlimit
END