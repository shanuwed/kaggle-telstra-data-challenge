## 12/19/2015
## 

# clear all objects from memory
rm(list = ls())

setwd('/home/user/src/telstra/data')

require("e1071") # svm
require("caret") # confusionMatrix()

set.seed(1)

# explore data files and merge them:
# event_type.csv   
# resource_type.csv      
# severity_type.csv  
# train.csv
# log_feature.csv  
# test.csv

temp = list.files(pattern="*.csv")
for (i in 1:length(temp)) assign(temp[i], read.csv(temp[i]))

str(severity_type.csv)
summary(severity_type.csv)

summary(train.csv)
hist(train.csv$fault_severity)
unique(train.csv$fault_severity)

summary(sample_submission.csv)
summary(resource_type.csv)
unique(resource_type.csv$resource_type)
summary(log_feature.csv)
hist(log_feature.csv$volume)
plot(log_feature.csv$volume)

length(unique(log_feature.csv$log_feature))
summary(event_type.csv)
length(unique(event_type.csv$event_type))

# train merge
train_merged <- merge(train.csv, event_type.csv, by=c("id", "id"))
train_merged <- merge(train_merged, log_feature.csv, by=c('id', 'id'))
train_merged <- merge(train_merged, resource_type.csv, by=c('id', 'id'))
train_merged <- merge(train_merged, severity_type.csv, by=c('id', 'id'))
str(train_merged)
length(unique(train_merged$id))

# test merge
test_merged <- merge(test.csv, event_type.csv, by=c("id", "id"))
test_merged <- merge(test_merged, log_feature.csv, by=c('id', 'id'))
test_merged <- merge(test_merged, resource_type.csv, by=c('id', 'id'))
test_merged <- merge(test_merged, severity_type.csv, by=c('id', 'id'))
str(test_merged)
length(unique(test_merged$id))

train_merged2 <- train_merged
train_merged2$train <- 1
test_merged2 <- test_merged
test_merged2$train <- 0
test_merged2$fault_severity <- 0

# merge two data frames vertically and save
merged_df <- rbind(train_merged2, test_merged2)
write.csv(merged_df, "tel_merged.csv")

