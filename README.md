# Introduction

This project contains data files, R source files and U-SQL scripts used to create my benchmark submission for Kaggle Telstra data challenge. The U-SQL reducer is used to transform a merged dataset to a discretized dataset.

### Data
Contains various data files used by the R script and the reducer.

### R
The tel_data_prep.R file produces tel_merged.csv which is a result of merging all data files (train.csv, test.csv, log_feature.csv, resource_type.csv, severity_type.csv), which becomes the input to the reducer. The submit_xgb_3.R file uses XGB (Extreme Gradient Boost) classifier to train and generate a prediction output file.

### Reducer_USQL
Contains the source code for the reducer written in U-SQL. Runs as a local job (means it would not require Azure account to run it.) The reducer makes the following transformation to tel_merged.csv:

- Row number: Removed
- id: transformed to id column
- location: transformed to loc column
- fault_severity: transformed to fs. Multinomial target variable.
- event_type: transformed to et_1 through et_53
- log_feature: transformed to lf_1 through lf_386
- volume: becomes the value for lf columns
- resource_type: transformed to rt_1 through rt_10
- severity_type: transformed to st column
- train: 1 indicates that the row is a train dataset. 0 indicates that the row is a test dataset

