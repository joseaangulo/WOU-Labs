boy(abraham).
boy(clancy).
boy(herb).
boy(homer).
boy(bart).
girl(mona).
girl(jackie).
girl(marge).
girl(patty).
girl(selma).
girl(lisa).
girl(maggie).
girl(ling).
parent_of(abraham, herb).
parent_of(abraham, homer).
parent_of(mona, homer).
parent_of(clancy, marge).
parent_of(clancy, patty).
parent_of(clancy, selma).
parent_of(jackie, marge).
parent_of(jackie, patty).
parent_of(jackie, selma).
parent_of(homer, bart).
parent_of(homer, lisa).
parent_of(homer, maggie).
parent_of(marge, bart).
parent_of(marge, lisa).
parent_of(marge, maggie).
parent_of(selma, ling).
father_of(X, Y) :- parent_of(X, Y), boy(X).
mother_of(X, Y) :- parent_of(X, Y), girl(X).
grandfather_of(X,Y) :- parent_of(X, Z), parent_of(Z, Y), boy(X).
grandmother_of(X, Y) :- parent_of(X, Z), parent_of(Z, Y), girl(X).
sister_of(X, Y) :- father_of(Z, X), mother_of(W, X), father_of(Z,Y), mother_of(W,Y), girl(X), X\==Y.
aunt_of(X, Y) :- parent_of(Z, Y), sister_of(X, Z).
brother_of(X, Y) :- father_of(Z, X), mother_of(W, X), father_of(Z,Y), mother_of(W,Y), boy(X), X\==Y.
uncle_of(X, Z) :- parent_of(Y, Z), brother_of(X, Y).
grandparent_of(X, Y) :- grandfather_of(X, Y) ; grandmother_of(X,Y).
siblings_of(X,Y) :- brother_of(X,Y) ; sister_of(X,Y).

%
%
%
%
%1. grandmother_of(X, bart).
%2. grandparent_of(abraham, X).
%3. aunt_of(X, lisa).
%4. grandparent_of(X, lisa).
%5. siblings_of(X, lisa).
%
%
%

next_to(X,Y,List):-is_right(X,Y,List).
next_to(X,Y,List):-is_right(Y,X,List).
is_right(L, R, [L | [R | _]]). 
is_right(L, R, [_ | Rest]) :- is_right(L, R, Rest).

owns_owl(Street, Who) :-
    Street = [_House1, _House2, _House3, _House4, _House5],
    member(house(red,osu_alum,_,_,_), Street),
    member(house(_,oit_alum,dog,_,_), Street),
    member(house(green,_,_,coffee,_), Street),
    member(house(_,uofo_alum,_,tea,_), Street),
    is_right(house(green,_,_,_,_), house(ivory,_,_,_,_), Street),
    member(house(ivory,_,_,_,_), Street),
    member(house(_,_,snails,_,cookies), Street),
    member(house(yellow,_,_,_,twinkies), Street),
    [_,_,house(_,_,_,milk,_),_,_] = Street,
    [house(_,psu_alum,_,_,_),_,_,_,_] = Street,
    next_to(house(_,_,_,_,pie), house(_,_,fox,_,_), Street),
    next_to(house(_,_,_,_,twinkies), house(_,_,horse,_,_), Street),
    member(house(_,_,_,orange_juice, ice_cream), Street),
    member(house(_,wou_alum,_,_,cheesecake), Street),
    next_to(house(_,psu_alum,_,_,_), house(blue,_,_,_,_), Street),
    member(house(_,Who,owl,_,_),Street).
    
    
    
    

