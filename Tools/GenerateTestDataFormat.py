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
    
    parser.add_argument('promptFilePath', metavar='p', type=str,
        help='path to test data prompts file')
    
    parser.add_argument('completionsFilePath', metavar='c', type=str,
        help='path to test data completions file')
    
    parser.add_argument('-o', '--out_put', type=bool,
        help='boolean to create output or not')
    
    print("parsing file")
    args = parser.parse_args()
    
    print("loading content")
    createOutPut = getCreateOutputValue(args.out_put)
    prompts = loadUnfilteredData(args.promptFilePath)
    completions = loadUnfilteredData(args.completionsFilePath)
    
    testData = generateTestData(prompts, completions)
    
    for data in testData:
        print(data)
    
    if createOutPut:
        print("creating output file at current path")
        outputJSONLFile(testData)
    
    return

def getCreateOutputValue(user_input:str) -> bool:
    return user_input.lower() in ("yes", "true", "t", "1", "y")

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


def combinePromptAndCompletion(prompt: str, completion: str) -> str:
    filtered_completion = completion.replace("\"", "\\\"")
    finalString = "{\"prompt\": \"" + prompt + "\", \"completion\": \"" + filtered_completion + "\"}"
        
    return finalString  
  

def generateTestData(prompts: list[str], completions: list[str]) -> list[str]:
    data = []
    
    for i in prompts.count():
        prompt = prompts[i]
        completion = completions[i]
        data.append(combinePromptAndCompletion(prompt, completion))
        
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


    
    


