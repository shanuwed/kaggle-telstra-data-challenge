## 1/1/2016
## 

# clear all objects from memory
rm(list = ls())

setwd('/home/user/src/telstra')

library(xgboost)
library(caret)
library(e1071)

source("tel_final_model.R")

set.seed(1)

#----Load the train dataset ----
wm <- read.csv('data/tel_reduced.csv', header=F)
names(wm) <- read.csv('data/header.csv', header = F, stringsAsFactors = F)

#---- Load location mapping ----
locmap <- read.csv('data/loc_vs_fs.csv', header=T)
locmap$Zero <- NULL
locmap$One <- NULL
locmap$Two <- NULL

wm <- merge(x=wm, y=locmap, by.x='loc', by.y='loc')

begin <- match('tr', names(wm))
wm[,begin:ncol(wm)] <- sapply(wm[,begin:ncol(wm)], as.numeric)

# Add the lf 
begin <- match('lf_1', names(wm))
end <- match('lf_386', names(wm))
wm$lfsum <- rowSums(wm[, begin:end])
wm$lfsum <- 1/(wm$lfsum+1) # for some reason it's responding to the inverse positively

# Add the count of et
begin <- match('et_1', names(wm))
end <- match('et_53', names(wm))
wm$etsum <- rowSums(wm[, begin:end])

# Add the count of rt
begin <- match('rt_1', names(wm))
end <- match('rt_10', names(wm))
wm$rtsum <- rowSums(wm[, begin:end])

# transform columns
# ...

# split data into train and test (using tr bit)
trainset <- wm[wm$tr==1,]
testset <-  wm[wm$tr==0,]

feature.names <- names(trainset)[5:ncol(trainset)]

clf <- xgboost(data        = data.matrix(trainset[,feature.names]),
               label       = trainset$fs,
               nrounds     = 20,
               objective   = "multi:softmax",
               num_class   = 3,
               eval_metric = "mlogloss")

testset$pred_xgb <- predict(clf, data.matrix(testset[,feature.names]))
write.csv(testset[testset$tr==0, c("id", "pred_xgb")], file="submission/pred_xgb_3.csv")
