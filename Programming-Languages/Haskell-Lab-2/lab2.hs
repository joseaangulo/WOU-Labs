gcdMine::Integeral a=>a->a->a
gcdMine x = 

gcdCheck x y = (myAnswer, correctAnswer, comment)
    where
    myAnswer = gcdMine x y
    correctAnswer = gcd x y
    comment = if myAnswer == correctAnswer then "Matches" else "Does not Match"