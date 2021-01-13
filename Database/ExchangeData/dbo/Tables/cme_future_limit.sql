﻿CREATE TABLE [dbo].[cme_future_limit] (
    [id]                                                                   BIGINT        IDENTITY (1, 1) NOT NULL,
    [run_id]                                                               BIGINT        NOT NULL,
    [create_date]                                                          DATETIME      NOT NULL,
    [create_user]                                                          VARCHAR (50)  NOT NULL,
    [last_update_date]                                                     DATETIME      NOT NULL,
    [last_update_user]                                                     VARCHAR (50)  NOT NULL,
    [contract_name]                                                        VARCHAR (255) NOT NULL,
    [rule_chapter]                                                         VARCHAR (255) NULL,
    [commodity_code]                                                       VARCHAR (255) NULL,
    [contract_size]                                                        VARCHAR (255) NULL,
    [contract_units]                                                       VARCHAR (255) NULL,
    [contract_type]                                                        VARCHAR (255) NULL,
    [settlement]                                                           VARCHAR (255) NULL,
    [contract_group]                                                       VARCHAR (255) NULL,
    [diminishing_balance_contract]                                         VARCHAR (255) NULL,
    [reporting_level]                                                      VARCHAR (255) NULL,
    [spot_month_position_comprised_of_futures_and_deliveries]              VARCHAR (255) NULL,
    [spot_month_aggregate_into_futures_equivalent_leg_1]                   VARCHAR (255) NULL,
    [spot_month_aggregate_into_futures_equivalent_leg_2]                   VARCHAR (255) NULL,
    [spot_month_aggregate_into_ratio_leg_1]                                VARCHAR (255) NULL,
    [spot_month_aggregate_into_ratio_leg_2]                                VARCHAR (255) NULL,
    [spot_Month_Accountability_Level]                                      VARCHAR (255) NULL,
    [daily_accountability_level_for_daily_contract]                        VARCHAR (255) NULL,
    [initial_spot_month_limit_in_net_futures_equivalents_leg_1_over_leg_2] VARCHAR (255) NULL,
    [initial_spot_month_limit_effective_date]                              VARCHAR (255) NULL,
    [spot_month_limit_in_contract_units_leg_1_over_leg_2]                  VARCHAR (255) NULL,
    [subsequent_spot_month_limits_in_net_futures_equivalents]              VARCHAR (255) NULL,
    [subsequent_spot_month_limits_effective_dates]                         VARCHAR (255) NULL,
    [single_month_aggregate_into_futures_equivalent_leg_1]                 VARCHAR (255) NULL,
    [single_month_aggregate_into_futures_equivalent_leg_2]                 VARCHAR (255) NULL,
    [single_month_aggregate_into_ratio_leg_1]                              VARCHAR (255) NULL,
    [single_month_aggregate_into_ratio_leg_2]                              VARCHAR (255) NULL,
    [single_month_accountability_level_leg_1_over_leg_2]                   VARCHAR (255) NULL,
    [single_month_limit_in_net_futures_equivalents_leg_1_over_leg_2]       VARCHAR (255) NULL,
    [all_month_aggregate_into_futures_equivalent_leg_1]                    VARCHAR (255) NULL,
    [all_month_aggregate_into_futures_equivalent_leg_2]                    VARCHAR (255) NULL,
    [all_month_aggregate_into_ratio_leg_1]                                 VARCHAR (255) NULL,
    [all_month_aggregate_into_ratio_leg_2]                                 VARCHAR (255) NULL,
    [all_month_accountability_level_leg_1_over_leg_2]                      VARCHAR (255) NULL,
    [all_month_limit_in_net_futures_equivalents_leg_1_over_leg_2]          VARCHAR (255) NULL
);

