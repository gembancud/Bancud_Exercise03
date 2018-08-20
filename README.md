# Bancud_Exercise03
Exercise#3] Create a new
solution <Lastname>_Exercise03 then add new projects for the following
problems.



Exercise03_A] 


 
 
  
  
  
  
  
  
  
  
  
  
  
  
 
 
 

 
(a) Write
a simple line editor. Keep the entire text on a linked list, one line in a
separate node. Start the program with entering EDIT file, after which a prompt appears along with the
line number. If the letter I is
entered with a number n
following it, then insert the text to be followed before line n. If I is not
followed by a number, then insert the text to be followed before line n. If I is not
followed by a number, then insert the text before the current line. If D is entered with two numbers n and m, one n, or numbers following it, then delete lines n through m, line n, or the current line. Do the same with the
command L, which stands for listing lines. If A is entered, then append the text to the
existing lines. 


 
(b)
Entry E signifies exit and saving the text in a file.
Here is an example:



EDIT testfile

1> The first line

2>

3> And another line

4> I 3

3> The second line

4> One more line

5> L

1> The first line

2>

3> The second line

4> One more line

5> And another line // This is now line 5, not 3;

5> D 2 // line 5, since L was issued from line 5;

4> L // line 4, since one line was deleted;

1> The first line

2> The second line // this and the following lines

3> One more line // now have new numbers;

4> And another line

4> E



 



Exercise03_B] A
two-dimensional shape can be defined by its boundary-polygon, which is simply a
list of all coordinates ordered by a traversal of its outline. See the
following figure for an example.




 




The left picture shows the original shape; the middle
picture, the outline of the shape. The rightmost picture shows an abstracted
boundary, using only the “most important” vertices. We can assign an importance
measure to a vertex 
 
 by
considering its neighbors 
 
 and 
 
. We compute the distances 
 
 and 
 
. Call these distances 
 
 and 
 
. Define the importance as 
 
 



Use the following algorithm to find the 
 
 most important points.



1.    
while the number of points is
greater than n



2.   
computer the importance of each
point



3.   
remove the least significant
one




 





 
(a) Write
a program to read set of coordinates (from an outline) and reduce the list to
the 
 
 most significant ones, where 
 
 is
an input value. 




 
(b) Draw
the initial and resulting shapes using WPF.




 
(c) Add
a slider showing each step of the simplification. Because a slider can go back
and forth, you have to store the results of each simplification step.



Note:
This problem and the algorithm for its solution are based on the paper: L. J.
Latecki and R. Lakämper, “Convexity Rule for Shape Decomposition Based on
Discrete Contour Evolution,” Computer
Vision and Image Understanding (CVIU) 73 (1999): 441-454.



 
