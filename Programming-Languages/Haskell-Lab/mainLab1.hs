squareRoot :: Double -> Double
squareRoot x = sqrt x

asciiPrevious :: Char -> Char
asciiPrevious x = pred x

verifyEven :: Integer -> Bool
verifyEven x = even (3*x + 1)

gaussianProduct :: Integer -> Integer
gaussianProduct n = product [1..n]


newList x = init (tail x)
largestInList x = maximum (newList x)

constructList = 6:19:41:(-3):[]
firstXEvens x = [ y | y <- [1..x*2], even y]

oddsDivisible3and7 y = [ x | x <- [1..(y-1)], odd x, mod x 7 == 0, mod x 3 == 0]

oddsDivisible9List y = [ x | x <- [100..y], odd x, mod x 9 == 0]
oddsDivisible9 x = length (oddsDivisible9List x)

countNegs a = [ x | x <- a, x < 0]

hexMaps = zip [0..]['0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F']

getSuit :: Int -> String
getSuit 3 = "Club"
getSuit 2 = "Spade"
getSuit 1 = "Diamond"
getSuit 0 = "Heart"
getSuit x = "Error"

dotProduct :: (Double,Double,Double) -> (Double,Double,Double) -> Double
dotProduct (x1, x2, x3) (y1, y2, y3) = (x1 * y1) + (x2 * y2) + (x3 * y3)

reverseFirstThree :: [a] -> [a]
reverseFirstThree [] = []
reverseFirstThree [x] = [x]
reverseFirstThree [x,y] = [y,x]
reverseFirstThree [x,y,z] = [z,y,x]
reverseFirstThree (x:y:z:xs) = z:y:x:xs

feelsLike :: Double -> String
feelsLike temp
  | temp <= (-100) = "Ice Age"
  | temp <= 0 = "Frostbite Central!"
  | temp <= 32 = "Freezing"
  | temp <= 60 = "Cool"
  | temp <= 80 = "Warm"
  | temp <= 110 = "Hot"
  | otherwise = "Burnin'"
  
xxs = [[1],[1,2],[1,2,3],[1,2,3,4],[1,2,3,4,5],[1,2,3,4,5,6],[1,2,3,4,5,6,7],[1,2,3,4,5,6,7,8],[1,2,3,4,5,6,7,8,9]]
makeList y = [[x | x <- xs] | xs <- xxs, y >= length xs]

sanitize a = [x | x <- a, x /= ' ']

feelsLike2 :: Double -> (Double,String)
feelsLike2 temperature
  | temp <= (-100) = (temp, "Ice Age")
  | temp <= 0 = (temp, "Frostbite Central!")
  | temp <= 32 = (temp, "Freezing")
  | temp <= 60 = (temp, "Cool") 
  | temp <= 80 = (temp, "Warm")
  | temp <= 110 = (temp, "Hot")
  | otherwise = (temp, "Burnin'")
  where temp = (temperature * 1.8) + 32

cylinder :: (RealFloat a) => a -> a -> a
cylinder a b = let squareRadius = a ^2 in pi * squareRadius * b
cylinderToVolume :: [(Double,Double)] -> [Double]
cylinderToVolume xs = [cylinder a b | (a,b) <- xs]

main=do 

--problem 1
putStrLn("")
putStrLn("-Problem 1-")
putStrLn("Input: 818281336460929553769504384519009121840452831049")
putStr("Output: ")
print(squareRoot 818281336460929553769504384519009121840452831049)
putStrLn("")

--Problem 2
putStrLn("-Problem 2-")
putStrLn("Input: 'A'")
putStr("Output: ")
print(asciiPrevious 'A')
putStrLn("")

--Problem 3
putStrLn("-Problem 3-")
putStrLn("Input1: 5")
putStr("Output1: ")
print(verifyEven 5)
putStrLn("Input2: 10")
putStr("Output2: ")
print(verifyEven 10)
putStrLn("Input3: 6541562")
putStr("Output3: ")
print(verifyEven 6541562)
putStrLn("")

--Problem 4
putStrLn("-Problem 4-")
putStrLn("Input: 100")
putStr("Output: ")
print(gaussianProduct 100)
putStrLn("")

--Problem 5
putStrLn("-Problem 5-")
putStrLn("Input: [99,23,4,2,67,82,49,-40]")
putStr("Output: ")
print(largestInList [99,23,4,2,67,82,49,-40])
putStrLn("")

--Problem 6
putStrLn("-Problem 6-")
putStr("Output: ")
print(constructList)
putStrLn("")

--Problem 7
putStrLn("-Problem 7-")
putStrLn("Input: 27")
putStr("Output: ")
print(firstXEvens 27)
putStrLn("")

--Problem 8
putStrLn("-Problem 8-")
putStrLn("Input: 200")
putStr("Output: ")
print(oddsDivisible3and7 200)
putStrLn("")

--Problem 9
putStrLn("-Problem 9-")
putStrLn("Input: 200")
putStr("Output: ")
print(oddsDivisible9 200)
putStrLn("")

--Problem 10
putStrLn("-Problem 10-")
putStrLn("Input: [(-4),6,7,8,(-14)]")
putStr("Output: ")
print(countNegs [(-4),6,7,8,(-14)])
putStrLn("")

--Problem 11
putStrLn("-Problem 11-")
putStr("Output: ")
print(hexMaps)
putStrLn("")

--Problem 12
putStrLn("-Problem 12-")
putStrLn("Input1: 7")
putStr("Output1: ")
print(makeList 7)
putStrLn("Input2: 0")
putStr("Output2: ")
print(makeList 0)
putStrLn("Input3: -1")
putStr("Output3: ")
print(makeList (-1))
putStrLn("")

--Problem 13
putStrLn("-Problem 13-")
putStrLn("Input: \"http://wou.edu/my homepage/I love spaces.html\"")
putStr("Output: ")
print(sanitize "http://wou.edu/my homepage/I love spaces.html")
putStrLn("")

--Problem 14
putStrLn("-Problem 14-")
{-
:t min
Ord a => a -> a -> a
:t length
Foldable t => t a -> Int
:t take
int -> [a] -> [a]
:t null
Foldable t => t a -> Bool
:t head
[a] -> a
-}
--Problem 15
putStrLn("-Problem 15-")
putStrLn("Input1: 0")
putStr("Output1: ")
print(getSuit 0)
putStrLn("Input2: 1")
putStr("Output2: ")
print(getSuit 1)
putStrLn("Input3: 2")
putStr("Output3: ")
print(getSuit 2)
putStrLn("Input4: 3")
putStr("Output4: ")
print(getSuit 3)
putStrLn("Input5: 77")
putStr("Output5: ")
print(getSuit 77)
putStrLn("")

--Problem 16
putStrLn("-Problem 16-")
putStrLn("Input: (1,2,3.0) (4.0,5,6)")
putStr("Output: ")
print(dotProduct (1,2,3.0) (4.0,5,6))
putStrLn("")

--Problem 17
putStrLn("-Problem 17-")
putStrLn("Input1: [1]")
putStr("Output1: ")
print(reverseFirstThree [1])
putStrLn("Input2: [1,2]")
putStr("Output2: ")
print(reverseFirstThree [1,2])
putStrLn("Input3: [1,2,3]")
putStr("Output3: ")
print(reverseFirstThree [1,2,3])
putStrLn("Input4: [1,2,3,4]")
putStr("Output4: ")
print(reverseFirstThree [1,2,3,4])
putStrLn("")

--Problem 18
putStrLn("-Problem 18-")
putStrLn("Input1: -200")
putStr("Output1: ")
print(feelsLike (-200))
putStrLn("Input2: 200")
putStr("Output2: ")
print(feelsLike 200)
putStrLn("Input3: -45.3")
putStr("Output3: ")
print(feelsLike (-45.3))
putStrLn("Input4: 79")
putStr("Output4: ")
print(feelsLike 79)
putStrLn("")

--Problem 19
putStrLn("-Problem 19-")
putStrLn("Input1: -200")
putStr("Output1: ")
print(feelsLike2 (-200))
putStrLn("Input2: -0.1")
putStr("Output2: ")
print(feelsLike2 (-0.1))
putStrLn("Input3: -42.9444444444444444444444444445")
putStr("Output3: ")
print(feelsLike2 (-42.9444444444444444444444444445))
putStrLn("Input4: 100")
putStr("Output4: ")
print(feelsLike2 100)
putStrLn("")

--Problem 20
putStrLn("-Problem 20-")
putStrLn("Input: [(2,5.3),(4.2,9),(1,1),(100.394)]")
putStr("Output: ")
print(cylinderToVolume [(2,5.3),(4.2,9),(1,1),(100.3,94)])
