int dice1 = 0, dice2 = 0, dice3 = 0, dice4 = 0, dice5 = 0;
bool fixed1, fixed2, fixed3, fixed4, fixes5;
int[] dice;
char input;
int handPlayer1 = 0;
int handPlayer2 = 0;
string hand = String.Empty;


PlayGame(1);
Console.WriteLine();
PlayGame(2);
Console.ForegroundColor = ConsoleColor.Cyan;
if (handPlayer1 > handPlayer2) { Console.WriteLine("Player1 wins!"); }
else if (handPlayer1 < handPlayer2) { Console.WriteLine("Player2 wins!"); }
else if (handPlayer1 == handPlayer2) { Console.WriteLine("Nobody wins!"); }
Console.ResetColor();

void PlayGame(int player)
{
    Console.WriteLine();
    Console.WriteLine($"Player {player}");
    Console.WriteLine("=========");
    fixed1 = fixed2 = fixed3 = fixed4 = fixes5 = false;

    for (int i = 1; i <= 3 && !(fixed1 && fixed2 && fixed3 && fixed4 && fixes5); i++)
    {
        RollDice();
        SortDice(dice1, dice2, dice3, dice4, dice5);
        PrintDice(i);
        if (i < 3) FixDice();
        if(i == 3 || fixed1==fixed2==fixed3==fixed4==fixes5==true)
        {
            AnalyzeAndPrintResult();
            if (player == 1) { handPlayer1 = DetermineWinner(handPlayer1); }
            else {handPlayer2= DetermineWinner(handPlayer2); }
        }

    }
    
}


void RollDice()
{
    if (fixed1 == false) dice1 = Random.Shared.Next(1, 7);
    if (fixed2 == false) dice2 = Random.Shared.Next(1, 7);
    if (fixed3 == false) dice3 = Random.Shared.Next(1, 7);
    if (fixed4 == false) dice4 = Random.Shared.Next(1, 7);
    if (fixes5 == false) dice5 = Random.Shared.Next(1, 7);
}

void PrintDice(int round)
{

    Console.Write($"Dice roll #{round}  (out of 3): {dice1}, {dice2}, {dice3}, {dice4}, {dice5}");
    Console.WriteLine();
}

void FixDice()
{
    Console.WriteLine("Which dice do you want to fix/unfix? (w-5, or 'r' to roll dice)");
    do
    {
        input = Convert.ToChar(Console.ReadLine()!);

        switch (input)
        {
            case '1': fixed1 = !fixed1; break;
            case '2': fixed2 = !fixed2; break;
            case '3': fixed3 = !fixed3; break;
            case '4': fixed4 = !fixed4; break;
            case '5': fixes5 = !fixes5; break;
            case 'r': break;
            default: Console.WriteLine("WHAATTT?"); break;
        }

        Console.Write("Fixed: ");
        if (fixed1) { Console.Write("1 "); }
        if (fixed2) { Console.Write("2 "); }
        if (fixed3) { Console.Write("3 "); }
        if (fixed4) { Console.Write("4 "); }
        if (fixes5) { Console.Write("5 "); }
    }
    while (input != 'r' && !(fixed1 && fixed2 && fixed3 && fixed4 && fixes5));
    Console.WriteLine();
}

void SortDice(int one, int two, int three, int four, int five)
{
    dice = new int[] { one, two, three, four, five };
    int temp;
    for (int i = 0; i <= dice.Length; i++)
    {
        for (int j = i + 1; j < dice.Length; j++)
        {
            if (dice[i] > dice[j])
            {
                temp = dice[i];
                dice[i] = dice[j];
                dice[j] = temp;
            }
        }
    }
    dice1 = dice[0];
    dice2 = dice[1];
    dice3 = dice[2];
    dice4 = dice[3];
    dice5 = dice[4];
}

void AnalyzeAndPrintResult()
{
    int counter1 = 0, counter2 = 0, counter3 = 0, counter4 = 0, counter5 = 0, counter6 = 0;
    hand = string.Empty;

    for (int a = 0; a < dice.Length; a++)
    {
        if (dice[a] == 1) { counter1++; }
        if (dice[a] == 2) { counter2++; }
        if (dice[a] == 3) { counter3++; }
        if (dice[a] == 4) { counter4++; }
        if (dice[a] == 5) { counter5++; }
        if (dice[a] == 6) { counter6++; }
    }

    bool fullHouse = counter1 == 3 || counter2 == 3 || counter3 == 3 || counter4 == 3 || counter5 == 3 || counter6 == 3;
    bool pair = counter1 == 2 || counter2 == 2 || counter3 == 2 || counter4 == 2 || counter5 == 2 || counter6 == 2;
    bool four_of_a_kind = counter1 == 4 || counter2 == 4 || counter3 == 4 || counter4 == 4 || counter5 == 4 || counter6 == 4;
    bool five_of_a_kind = counter1 == 5 || counter2 == 5 || counter3 == 5 || counter4 == 5 || counter5 == 5 || counter6 == 5;
    bool straight = counter1 == 1 || counter2 == 1 || counter3 == 1 || counter4 == 1 || counter5 == 1 || counter6 == 1;

    int pairs = 0;

    Console.ForegroundColor = ConsoleColor.Magenta;

    if (five_of_a_kind) { hand = "Five of a kind!"; }
    else if (four_of_a_kind) { hand = "Four of a kind!"; }
    else if (fullHouse && pair) { hand = "Full House!"; }
    else if (fullHouse) { hand = "Three of a kind!"; }
    else if (pair)
    {
        for (int a = 0; a < dice.Length; a++)
        {
            for (int b = a + 1; b < dice.Length; b++)
            {
                if (dice[a] == dice[b])
                {
                    pairs++;
                }
            }
        }
        if (pairs == 2) { hand = "Two Paires"; }
        else { hand = "One Pair"; }
    }

    else if (straight)
    {
        if (dice[0] == dice[1] - 1 && dice[1] == dice[2] - 1 && dice[2] == dice[3] - 1 && dice[3] == dice[4] - 1)
        {
            hand = "Straight!";
        }
        else { hand = "Bust!"; }
    }

    Console.WriteLine(hand);

    Console.ResetColor();
    Console.WriteLine();
}
int DetermineWinner(int stufe)
{
    if (hand == "Five of a kind!") { stufe = 8; }
    else if (hand == "Four of a kind!") { stufe = 7; }
    else if (hand == "Full House!") { stufe = 6; }
    else if (hand == "Three of a kind!") { stufe = 5; }
    else if (hand == "Two Paires") { stufe = 4; }
    else if (hand == "One Pair") { stufe = 3; }
    else if (hand == "Straight") { stufe = 2; }
    else { stufe = 1; }

    return stufe;
}



//Ngimbi Sarah