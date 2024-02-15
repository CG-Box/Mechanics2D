INCLUDE globals.ink

Hello there! #speaker:Student #layout:Right
-> main

=== main ===
Can you help me?
+ [Take Quest] #sound: questTakeSound
    -> QuestTaken
+ [Don't Help]->Mid
+ [Ignore]
    You are so egoistic!
    ->END

=== QuestTaken ===
~TakeQuest("SurviveQuest")
~ surviveQuestTakenTimes += 1
Thanks dude you helped me {surviveQuestTakenTimes} times
   -> END

=== Mid ===
Did you hear that?
+ [Strange]
    That makes me feel so <color=\#FF0000>creepy</color> !!
+ [Nothing]
    Oh, well that makes me feel <color=\#00FF00>better</color> too. 
    
- I hope it's over.

Well, do you think we can survive?
+ [Yes] #sound: questTakeSound
    Your are so cute.
    Now i'm happy!!
    -> END
+ [No]
    We all die!!
    -> END