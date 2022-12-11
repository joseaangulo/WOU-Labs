

newNumber x = 3*x + 1
isEven x = if (mod (newNumber x) 2 == 0) then True else False

newList x = init (tail x)

largestInList x = maximum (newList x)

constructList = 6:19:41:(-3):[]

firstXEvens y = [ x | x <- [1..y*2], even x]

oddsDivisible3and7 y = [ x | x <- [1..(y-1)], odd x, mod x 7 == 0, mod x 3 == 0]

oddsDivisible9List y = [ x | x <- [100..y], odd x, mod x 9 == 0]
oddsDivisible9 x = length (oddsDivisible9List x)

list y = [ x | x <- [ z | z <- [1..y] ]]

abc = "http://wou.edu/my homepage/I love spaces.html"
sanatize a = [x | x <- a, x /= ' ']
--sanatize a = drop " " a

getSuit :: Int -> String
getSuit 3 = "Club"
getSuit 2 = "Spade"
getSuit 1 = "Diamond"
getSuit 0 = "Heart"

dotProduct :: (Double,Double,Double) -> (Double,Double,Double) -> Double
dotProduct (x1, x2, x3) (y1, y2, y3) = (x1 * y1) + (x2 * y2) + (x3 * y3)

reverseFirstThree :: [a] -> [a]
reverseFirstThree [] = []
reverseFirstThree [x] = [x]
reverseFirstThree [x,y] = [y,x]
reverseFirstThree [x,y,z] = [z,y,x]
reverseFirstThree (x:y:z:xs) = z:y:x:xs

xxs = [[1],[1,2],[1,2,3],[1,2,3,4]]
makeList y = [[x | x <- xs] | xs <- xxs, y >= length xs]

feelsLike :: Double -> String
feelsLike temp
  | temp <= (-100) = "Ice Age"
  | temp <= 0 = "Frostbite Central!"
  | temp <= 32 = "Freezing"
  | temp <= 60 = "Cool"
  | temp <= 80 = "Warm"
  | temp <= 110 = "Hot"
  | otherwise = "Burnin'"

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
