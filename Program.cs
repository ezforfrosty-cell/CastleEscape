int width = 4;
int height = 3;

int maxHp = 10;
int hp = 10;
int px = 0;
int py = 0;
int score = 0;
int turn = 0;

bool hasSmallKey = false;
bool hasMedkit = false;
bool medkitUsed = false;
bool hasGateKey = false;

int gateX = 3;
int gateY = 0;
bool gateOpened = false;

int gx = 3;
int gy = 1;
bool guardLeft = true;
int damage = 3;

bool isWin = false;
bool isLose = false;
bool isExit = false;

Console.WriteLine("=== ПОБЕГ ИЗ ЗАМКА ===");
Console.WriteLine("Выбери сложность: 1 - легко, 2 - нормально, 3 - сложно");
Console.Write("> ");
string level = Console.ReadLine();

switch (level)
{
    case "1":
        damage = 2;
        Console.WriteLine("Сложность: легко (охранник бьёт на 2 HP)");
        break;
    case "2":
        damage = 3;
        Console.WriteLine("Сложность: нормально (охранник бьёт на 3 HP)");
        break;
    case "3":
        damage = 4;
        Console.WriteLine("Сложность: сложно (охранник бьёт на 4 HP)");
        break;
    default:
        damage = 3;
        Console.WriteLine("Нет такой сложности. Выбрана нормальная (3 HP)");
        break;
}

Console.WriteLine();
Console.WriteLine("Команды: w/a/s/d - идти, осмотр, ждать, аптечка, открыть, exit или q - выход");

while (!isWin && !isLose && !isExit)
{
    Console.WriteLine();
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            if (x == px && y == py)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[И]");
            }
            else if (x == gx && y == gy)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[О]");
            }
            else if (x == gateX && y == gateY)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[В]");
            }
            else
            {
                Console.Write("[ ]");
            }
            Console.ResetColor();

            if (x == 1)
            {
                Console.Write(" | ");
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine("И - игрок, О - охранник, В - ворота, | - запертая дверь");
    Console.WriteLine($"Ход: {turn}  HP: {hp}/{maxHp}  Счёт: {score}  Позиция: ({px}, {py})");

    Console.Write("> ");
    string input = Console.ReadLine();
    string command = input.Trim().ToLower();

    bool actionDone = false;
    int newX = px;
    int newY = py;

    switch (command)
    {
        case "w":
            newY = py - 1;
            actionDone = true;
            break;
        case "s":
            newY = py + 1;
            actionDone = true;
            break;
        case "a":
            newX = px - 1;
            actionDone = true;
            break;
        case "d":
            newX = px + 1;
            actionDone = true;
            break;

        case "осмотр":
            actionDone = true;
            if (px == 0 && py == 0 && !hasSmallKey)
            {
                hasSmallKey = true;
                score = score + 10;
                Console.WriteLine("Ты нашёл маленький ключ! (+10 очков)");
            }
            else if (px == 1 && py == 2 && !hasMedkit)
            {
                hasMedkit = true;
                score = score + 10;
                Console.WriteLine("Ты нашёл аптечку! (+10 очков)");
            }
            else if (px == 3 && py == 2 && !hasGateKey)
            {
                hasGateKey = true;
                score = score + 20;
                Console.WriteLine("Ты нашёл ключ от главных ворот! (+20 очков)");
            }
            else
            {
                Console.WriteLine("Здесь ничего полезного нет.");
            }
            break;

        case "аптечка":
            if (!hasMedkit)
            {
                Console.WriteLine("У тебя нет аптечки.");
            }
            else if (medkitUsed)
            {
                Console.WriteLine("Аптечка уже использована.");
            }
            else if (hp == maxHp)
            {
                Console.WriteLine("Здоровье и так полное.");
            }
            else
            {
                hp = hp + 4;
                if (hp > maxHp)
                {
                    hp = maxHp;
                }
                medkitUsed = true;
                actionDone = true;
                Console.WriteLine($"Ты использовал аптечку. HP: {hp}");
            }
            break;

        case "открыть":
            actionDone = true;
            if (px != gateX || py != gateY)
            {
                Console.WriteLine("Здесь нет ворот.");
            }
            else if (!hasGateKey)
            {
                Console.WriteLine("Ворота заперты. Нужен ключ от главных ворот.");
            }
            else
            {
                gateOpened = true;
                Console.WriteLine("Ключ подошёл, ворота открываются...");
            }
            break;

        case "ждать":
            actionDone = true;
            Console.WriteLine("Ты затаился и ждёшь...");
            break;

        case "exit":
        case "q":
            isExit = true;
            break;

        default:
            Console.WriteLine($"Неизвестная команда \"{command}\".");
            Console.WriteLine("Команды: w/a/s/d, осмотр, ждать, аптечка, открыть, exit или q");
            break;
    }

    if (newX != px || newY != py)
    {
        if (newX < 0)
        {
            Console.WriteLine("Слева стена, туда нельзя.");
        }
        else if (newX >= width)
        {
            Console.WriteLine("Справа стена, туда нельзя.");
        }
        else if (newY < 0)
        {
            Console.WriteLine("Сверху стена, туда нельзя.");
        }
        else if (newY >= height)
        {
            Console.WriteLine("Снизу стена, туда нельзя.");
        }
        else if (px == 1 && newX == 2 && !hasSmallKey)
        {
            Console.WriteLine("Дверь заперта. Нужен маленький ключ.");
        }
        else
        {
            if (px == 1 && newX == 2)
            {
                Console.WriteLine("Ты открыл дверь маленьким ключом.");
            }
            px = newX;
            py = newY;
            Console.WriteLine($"Ты перешёл в комнату ({px}, {py}).");
        }
    }

    if (actionDone)
    {
        turn = turn + 1;

        if (guardLeft)
        {
            gx = gx - 1;
        }
        else
        {
            gx = gx + 1;
        }

        if (gx < 0)
        {
            gx = 1;
            guardLeft = false;
        }
        else if (gx >= width)
        {
            gx = width - 2;
            guardLeft = true;
        }

        if (gx == px && gy == py)
        {
            hp = hp - damage;
            if (hp < 0)
            {
                hp = 0;
            }
            score = score - 5;
            if (score < 0)
            {
                score = 0;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Охранник тебя заметил! -{damage} HP");
            Console.ResetColor();

            if (guardLeft)
            {
                guardLeft = false;
                gx = gx + 1;
            }
            else
            {
                guardLeft = true;
                gx = gx - 1;
            }
        }
    }

    if (hp <= 0)
    {
        isLose = true;
    }
    else if (gateOpened && hasGateKey && px == gateX && py == gateY)
    {
        isWin = true;
    }
    else if (hp <= 3)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("ВНИМАНИЕ: здоровье ниже 30%! Используй аптечку.");
        Console.ResetColor();
    }
}

Console.WriteLine();
if (isWin)
{
    score = score + 50;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("ПОБЕДА! Ты сбежал из замка!");
}
else if (isLose)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("ПОРАЖЕНИЕ. Охрана тебя поймала.");
}
else
{
    Console.WriteLine("Ты вышел из игры. До встречи!");
}
Console.ResetColor();
Console.WriteLine($"Итоговый счёт: {score}  Ходов: {turn}  HP: {hp}");
