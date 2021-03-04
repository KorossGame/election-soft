from datetime import datetime
import random
import string
import subprocess
import sys
import os
import difflib

# Files to work with
testInputTextFile=os.getcwd()+"\\bin\\Release\\netcoreapp3.1\\Assessment1Data.txt"
testOutputTextFile=os.getcwd()+"\\bin\\Release\\netcoreapp3.1\\Assessment1TestResults.txt"
testSolutionTextFile=os.getcwd()+"\\bin\\Release\\netcoreapp3.1\\Assessment1Solution.txt"

numberOfTestCases = int(input("Number of test cases: "))

successTests = 0
failedTests = 0

caseParties = []
caseDataToWrite = []

def generateTheTest():
    # Reset everything
    nameOfElection=""
    seatsToAllocate = 0
    totalVotes = 0
    
    global caseParties
    global caseDataToWrite
    caseParties = []
    caseDataToWrite = []
    
    parties = []
    letters = string.ascii_lowercase

    # Generate data
    nameOfElection = "#" + datetime.now().strftime("%d-%m-%Y-%H-%M-%S-") + (''.join(random.choice(letters) for i in range(10)))
    
    # Should we have an error
    shouldBeDataCorrupted = random.choice([True, False])
    
    if shouldBeDataCorrupted:
        seatsToAllocate = random.randint(-1000,1000)
        totalVotes = random.randint(-1000000,1000000)
    else:
        seatsToAllocate = random.randint(0,10)
        totalVotes = random.randint(0,1000000)

    # To allocate equal number of votes in normal case scenario
    votesLeft = totalVotes

    # Number of parties
    randomNumberOfParties = random.randint(0,10)
    names = []
    
    # Generate parties
    for i in range(0, randomNumberOfParties):
        generated = False

        while not generated:
            partyName = (''.join(random.choice(letters) for i in range(5)))
            if partyName in names:
                pass
            else:
                names.append(partyName)
                generated = True

        if shouldBeDataCorrupted:
            partyVotes=random.randint(-10000,10000)
        else:
            partyVotes=random.randint(0,votesLeft)
            votesLeft-=partyVotes

        partySeats = ""
        nameOfSeats = partyName.upper()

        numberOfSeats = random.randint(1,5)

        for i in range(0, numberOfSeats):
            partySeats+=","+nameOfSeats+str(i)
        parties.append(partyName+","+str(partyVotes)+partySeats+";"+"\n")

        caseParties.append([partyName, partyVotes, partySeats[1:].split(',')])
    
    # Election info
    dataToWrite = nameOfElection+"\n"+str(seatsToAllocate)+"\n"+str(totalVotes)+"\n"

    # Copy data to global for generation solution file
    caseDataToWrite.append(nameOfElection)
    caseDataToWrite.append(seatsToAllocate)
    caseDataToWrite.append(totalVotes)

    # Write to input file
    f = open(testInputTextFile, 'w')
    f.writelines(dataToWrite)
    f.writelines(parties)
    f.close()

def generateTheSolution():
    allocatedSeats = []

    for party in caseParties:
        allocatedSeats.append(0)

    for seat in range(1, caseDataToWrite[1]+1):
        maxVotes=-100000
        maxVotesParties=[]
        
        for i in range(0,len(caseParties)):
            #print(caseParties[i][0]+"\t"+str(caseParties[i][1]));
            if caseParties[i][1]>maxVotes and allocatedSeats[i] < len(caseParties[i][2]):
                # Set new max
                maxVotes = caseParties[i][1]
                
                # Clear previous winners
                maxVotesParties = []
                maxVotesParties.append(i)
                
            elif caseParties[i][1]==maxVotes and allocatedSeats[i] < len(caseParties[i][2]):
                maxVotesParties.append(i)

        #print(maxVotesParties)
        if (len(maxVotesParties) > 0):
            winningIndex = random.randint(0, len(maxVotesParties)-1);
            win = maxVotesParties[winningIndex]
        
            allocatedSeats[win]+=1
            caseParties[win][1]=(caseParties[win][1]*allocatedSeats[win]) // (seat+1)
        #print(allocatedSeats)
        #print("--------------")
    dataToWrite = caseDataToWrite[0]+"\n"

    global solution
    solution=""
    for i in range(0, len(caseParties)):
        if (allocatedSeats[i]!=0):
            solution+=caseParties[i][0]
            for j in range(0, allocatedSeats[i]):
                solution+=","+caseParties[i][2][j]
            solution+=";\n"
    
    # Write to solution file
    f = open(testSolutionTextFile, 'w')
    f.writelines(dataToWrite)
    f.writelines(solution)
    f.close()

def runTheTest():
    #Run the program
    bashCommand = os.getcwd()+"\\bin\\Release\\netcoreapp3.1\\ElectionSoft";
    result = subprocess.run(bashCommand, capture_output=True, text=True, check=True);

def compareResultWithSolution():
    difference = ""
    data0=open(testInputTextFile).readlines()
    data1=open(testOutputTextFile).readlines()
    data2=open(testSolutionTextFile).readlines()
    
    for line in difflib.unified_diff(data1, data2):
        difference+=line

    global successTests, failedTests
    
    # If any difference - failed
    if (difference=="" or difference == None):
        successTests+=1
    else:
        failedTests+=1
        print(difference)
        
    

def main():
    for i in range (0,numberOfTestCases):
        generateTheTest()
        generateTheSolution()
        runTheTest()
        compareResultWithSolution()

    print("Success: "+str(successTests))
    print("Failed: "+str(failedTests))
main()
