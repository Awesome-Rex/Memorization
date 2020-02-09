from time import sleep
from random import randint, sample, choice
from tkinter import *
#import pygame

window = Tk()
window.title("Commands")
window.configure(background="#FFFFFF")
window.wm_attributes("-topmost", 1)

canvas = Canvas(window, width=1024, height=125, bg="Yellow", bd=0, highlightthickness=0)
canvas.pack()

window.update()

def all_print(txt_in):
    print(txt_in)

    canvas.delete("all")
    canvas.create_text(512, 63, text=txt_in, fill="black", font=('Times', 50))
    window.update_idletasks()
    window.update()

numbers = ["Hana", "Dul", "Set", "Net", "Dasot", "Yosot", "Ilgop", "Yodol", "Ahop", "Yol"]

stances = {"Walking" : "Gunnon", "Sitting" : "Annun", "L" : "Niunja"}
moves = { 
    "Punch" : "Jirugi",
    "High Punch" : "Napunde Jirugi",

    "Forearm Guarding Block" : "Palmok Daebi Makgi",
    "Knife-Hand Guarding Block" : "Sonkal Daebi Makgi",

    "Inner Forearm Middle Block" : "An Palmok Kuande Makgi",

    "Low Forearm Block" : "Palmok Najunde Makgi",
    "Low Knifehand Block" : "Sonkal Najunde Makgi",

    "Twin-Outer Forearm Block" : "Sang Palmok Makgi",
    "Twin-Outer Knifehand Block" : "Sang Sonkal Makgi",

    "Forearm Rising Block" : "Palmok Chukyo Makgi",
    "Knife-Hand Rising Block" : "Sonkal Chukyo Makgi",

    "Outer Forearm High Side Block" : "Bakat Palmok Nopunde Yop Makgi",
    "Outer Forearm High Wedge Block" : "Bakat Palmok Nopunde Hechyo Makgi",

    "Knife-Hand Middle Side Strike" : "Sonkal Kuande Yop Taerigi",

    "Straight Fingertip Thrust" : "Sun Sonkut Tulgi",

    "Backfist High Side Strike" : "Dung Joomuk Nopunde Yop Taerigi",

    "Side Piercing Kick" : "Yop Chagi",
    "Front-Snap Kick" : "Ap Chagi",
    "Turning Kick" : "Dollyo Chagi",
    "Spinning Hook Kick" : "Huryeo Chagi"
}

kicks = ["Front Snap Kick", "Side Piercing Kick", "Turning Kick", "Double Motion Front Snap Kick"]
strides = ["Strides", "Stride Jumps"]

exercises = [
    str(randint(40, 45)) + " push-ups",
    str(randint(30, 35)) + " sit-ups",
    "40 " + kicks[randint(0, len(kicks) - 1)] + "s",
    "10 " + "Jump Clap Push-ups",
    "50 " + str(sample(strides, 1))
]

patterns = ["Saju Jirugi", "Saju Makgi", "Chon-Ji", "Dan Gun", "Do-San"]

end_activities = ["Sparring", "3-Step Sparring #" + str(randint(1, 3)), "Self Defence", "1-Step Free Sparring"]

#normal command
def com (command):
    all_print(command)

    response = input()

    return

#translate technique command
def trans_tech_com ():
    stance = choice(list(stances.keys()))
    move = choice(list(moves.keys()))

    maxMoves = 1;
        
    if randint(1, 5) == 1:
        maxMoves = randint(1, 2) * 5;
        
        #for num in range(0, maxMoves):
        #   all_print(numbers[num])

        #  sleep(1)

    
    all_print(stances[stance] + " so " + moves[move] + " (x" + str(maxMoves) + ")")

    response = input()
    
    while response != " " and response != "r" and response != "/":
        all_print("Re-enter response please!")
        sleep(1)
        all_print(stances[stance] + " so " + moves[move])
        
        response = input()

    if response == " " or response == "":
        #
        #
        #
        return maxMoves
    elif response == "r":
        return trans_tech_com()
    elif response == "/":
        all_print(stance + " " + move)

        response = input()
        return 1
    
def trans_quest_com (question, answer):
    pass

def attention_stance():
    all_print ("Charyot Sogi")
    sleep(3)
    all_print ("Kyong Ye")
    sleep(2)
def sitting_stance():
    print ("Annun Sogi")
    print ("")
    
    sleep(3)
    
    for num in range(0, 10):
        print (numbers[num])
        sleep(1)
def walking_stance():
    print("Gunnon Sogi")
    print ("")

    

commands = [
    attention_stance,
    sitting_stance,
    walking_stance
]

#Execution

try:
    attention_stance()
    print("")

    #oath
    if randint(1, 3) == 1:
        com("Oath: ")

    #exercise

    exercises_done = sample(exercises, 3)

    com(exercises_done[0])
    com(exercises_done[1])
    com(exercises_done[2])
    
    #technique
    #for i in range(randint(5, 15)):
    i = 5
    while i <= randint(10, 25):
        i += trans_tech_com()
        
    #patterns
    patterns_to_pract = sample(patterns, 2)

    com("Do " + patterns_to_pract[0])
    com("Do " + patterns_to_pract[1])
    
    #sparring/3-step sparring/self defence
    com(end_activities[randint(0, len(end_activities) - 1)])
    
    com("Study on Anki")
    
    all_print ("Training is done")
except KeyboardInterrupt:
    all_print("Training is done")
