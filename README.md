# Unzip-compressed-files-from-Azure-Storage-using-Azure-Functions
Unzip compressed files from Azure Storage using Azure Functions

Using Azure Functions v1.x

## Pre-requisites and Guided Steps
- The functin trigger should be set as "Blob trigger" to pick compressed files from {your blob}\tempdaily\zip folder, no harm using any other location for this as it is not dependent on anything in the code per se. 
- Make sure you have a Azure Sotrage Blob with a folder named "tempdaily". If not change the name in line 16 to your folder name.
- The code also unzips all the compressed files to location "tempdaily\unzip". Change the target location as necessary in line 31.
