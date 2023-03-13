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
    finalString = "{\"prompt\": \"" + "" + "\", " + "\"completion\": \"" + choice + "\"}"
        
    return finalString  
  

def generateTestData(content: list[str]) -> list[str]:
    PROMPT = "Create a lyric based on what Kendrick Lamar would sound like."
    data = []
    
    for line in content:
        cleanLine = line.strip()
        result = createPromptBasedChoice(PROMPT, cleanLine)
        data.append(result)
        
    return data

def outputJSONLFile(data: list[str]):
    print("creating output file")
    fp = open('rapDataKendricLyrics2.jsonl', 'w')
    
    for d in data:
        fp.write(d)
        fp.write("\n")
    fp.close()

if __name__ == "__main__":
    main()


    
    

