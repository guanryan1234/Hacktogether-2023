# -*- coding: utf-8 -*-
"""
Test for fine tuning a OpenAi model
"""

import argparse
from pathlib import Path

def main ():
    print("starting app...")
    
    parser = argparse.ArgumentParser(
        description='locate a line by line test data file')
    
    parser.add_argument('filePath', metavar='f', type=str,
        help='path to test data file')
    
    parser.add_argument('createOutput', metavar='o', type=bool,
        help='boolean to create output or not')
    
    print("parsing file")
    args = parser.parse_args()
    
    print("loading content")
    content = loadUnfilteredData(args.filePath)
    
    testData = generateTestData(content)
    
    for data in testData:
        print(data)
    
    if args.createOutput:
        outputJSONLFile(testData)
    
    return
    
def checkIfFileExist(filePath: str) -> bool:
    # check if file exist 
    path = Path(filePath)
    if path.is_file():
        return True 
    else:
        return False
    
def loadUnfilteredData(filePath: str) -> str:
    content = ""
    
    if checkIfFileExist(filePath):
        f = open(filePath, 'r')
        content = f.readlines()
        print(content)
        
        if not content or content == "":
            raise ValueError("empty data set")
        else:
            return content
    else:
        raise FileNotFoundError("given file does not exist")


def createPromptBasedChoice(prompt: str, choice: str) -> str:
    finalString = "{\"prompt\": \"" + prompt + "\", " + "\"completion\": \"" + choice + "\"}"
        
    return finalString  
  

def generateTestData(content: list[str]) -> list[str]:
    DEFAULT_PROMPT = ""
    choice = "";
    data = []
    
    for line in content:
        prompt = line.strip()
        result = createPromptBasedChoice(prompt, choice)
        data.append(result)
        
    return data

def outputJSONLFile(data: list[str]):
    print("creating output file")
    fp = open('TrainingData.jsonl', 'w')
    
    for d in data:
        fp.write(d)
        fp.write("\n")
    fp.close()

if __name__ == "__main__":
    main()


    
    


