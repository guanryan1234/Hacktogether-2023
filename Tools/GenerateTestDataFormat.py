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
    
    parser.add_argument('-o', '--out_put', type=bool,
        help='boolean to create output or not')
    
    parser.add_argument('-a', '--add_completions', type=bool,
        help='boolean to create output or not')
    
    print("parsing file")
    args = parser.parse_args()
    
    print("loading content")
    content = loadUnfilteredData(args.filePath)
    
    testData = generateTestData(content, args.add_completions)
    
    for data in testData:
        print(data)
    
    if args.out_put:
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
        
        if not content or content == "":
            raise ValueError("empty data set")
        else:
            return content
    else:
        raise FileNotFoundError("given file does not exist")


def createPromptBasedChoice(prompt: str, choice: str) -> str:
    finalString = "{\"prompt\": \"Respond with a one word \"True\" or \"False\" answer for the following question. Based on this senetence \"" + prompt + "\" is the user trying to schedule a meeting? " + "\"completion\": \"" + choice + "\"}"
        
    return finalString  
  

def generateTestData(content: list[str], add_completion: bool) -> list[str]:
    DEFAULT_PROMPT = ""
    choice = "";
    data = []
    
    for line in content:
        prompt = line.strip()

        if(add_completion):
            choice = inputCompletion(prompt)

        result = createPromptBasedChoice(prompt, choice)
        data.append(result)
        
    return data

def inputCompletion(prompt: str) -> str:
    choice = ""

    print("PROMPT: " + prompt)
    print("ENTER COMPLETION: ")
    choice = input()

    return choice

def outputJSONLFile(data: list[str]):
    print("creating output file")
    fp = open('TrainingData.jsonl', 'w')
    
    for d in data:
        fp.write(d)
        fp.write("\n")
    fp.close()

if __name__ == "__main__":
    main()


    
    


