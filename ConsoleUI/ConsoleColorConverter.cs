﻿//[ESC]
//[
//3
//8
//;
//2
//;
//R1
//R2
//R3
//;
//G1
//G2
//G3
//;
//B1
//B2
//B3
//m
//Char

namespace ConsoleUI;

public static class ConsoleColorConverter
{
    public static Dictionary<ConsoleColor, (int r, int g, int b)> associations = new()
    {
        {ConsoleColor.Black, (0x00, 0x00, 0x00)},
        {ConsoleColor.DarkBlue, (0x00, 0x00, 0x80)},
        {ConsoleColor.DarkGreen, (0x00, 0x80, 0x00)},
        {ConsoleColor.DarkCyan, (0x00, 0x80, 0x80)},
        {ConsoleColor.DarkRed, (0x80, 0x00, 0x00)},
        {ConsoleColor.DarkMagenta, (0x80, 0x00, 0x80)},
        {ConsoleColor.DarkYellow, (0x80, 0x80, 0x00)},
        {ConsoleColor.DarkGray, (0x80, 0x80, 0x80)},
        {ConsoleColor.Blue, (0x00, 0x00, 0xFF)},
        {ConsoleColor.Green, (0x00, 0xFF, 0x00)},
        {ConsoleColor.Cyan, (0x00, 0xFF, 0xFF)},
        {ConsoleColor.Red, (0xFF, 0x00, 0x00)},
        {ConsoleColor.Magenta, (0xFF, 0x00, 0xFF)},
        {ConsoleColor.Yellow, (0xFF, 0xFF, 0x00)},
        {ConsoleColor.Gray, (0xC0, 0xC0, 0xC0)},
        {ConsoleColor.White, (0xFF, 0xFF, 0xFF)}
    };

    static Dictionary<int, string> intLookupStringTable = new()
    {
        {000, "000"}, {001, "001"}, {002, "002"}, {003, "003"}, {004, "004"}, {005, "005"}, {006, "006"},
        {007, "007"}, {008, "008"}, {009, "009"}, {010, "010"}, {011, "011"}, {012, "012"}, {013, "013"},
        {014, "014"}, {015, "015"}, {016, "016"}, {017, "017"}, {018, "018"}, {019, "019"}, {020, "020"},
        {021, "021"}, {022, "022"}, {023, "023"}, {024, "024"}, {025, "025"}, {026, "026"}, {027, "027"},
        {028, "028"}, {029, "029"}, {030, "030"}, {031, "031"}, {032, "032"}, {033, "033"}, {034, "034"},
        {035, "035"}, {036, "036"}, {037, "037"}, {038, "038"}, {039, "039"}, {040, "040"}, {041, "041"},
        {042, "042"}, {043, "043"}, {044, "044"}, {045, "045"}, {046, "046"}, {047, "047"}, {048, "048"},
        {049, "049"}, {050, "050"}, {051, "051"}, {052, "052"}, {053, "053"}, {054, "054"}, {055, "055"},
        {056, "056"}, {057, "057"}, {058, "058"}, {059, "059"}, {060, "060"}, {061, "061"}, {062, "062"},
        {063, "063"}, {064, "064"}, {065, "065"}, {066, "066"}, {067, "067"}, {068, "068"}, {069, "069"},
        {070, "070"}, {071, "071"}, {072, "072"}, {073, "073"}, {074, "074"}, {075, "075"}, {076, "076"},
        {077, "077"}, {078, "078"}, {079, "079"}, {080, "080"}, {081, "081"}, {082, "082"}, {083, "083"},
        {084, "084"}, {085, "085"}, {086, "086"}, {087, "087"}, {088, "088"}, {089, "089"}, {090, "090"},
        {091, "091"}, {092, "092"}, {093, "093"}, {094, "094"}, {095, "095"}, {096, "096"}, {097, "097"},
        {098, "098"}, {099, "099"}, {100, "100"}, {101, "101"}, {102, "102"}, {103, "103"}, {104, "104"},
        {105, "105"}, {106, "106"}, {107, "107"}, {108, "108"}, {109, "109"}, {110, "110"}, {111, "111"},
        {112, "112"}, {113, "113"}, {114, "114"}, {115, "115"}, {116, "116"}, {117, "117"}, {118, "118"},
        {119, "119"}, {120, "120"}, {121, "121"}, {122, "122"}, {123, "123"}, {124, "124"}, {125, "125"},
        {126, "126"}, {127, "127"}, {128, "128"}, {129, "129"}, {130, "130"}, {131, "131"}, {132, "132"},
        {133, "133"}, {134, "134"}, {135, "135"}, {136, "136"}, {137, "137"}, {138, "138"}, {139, "139"},
        {140, "140"}, {141, "141"}, {142, "142"}, {143, "143"}, {144, "144"}, {145, "145"}, {146, "146"},
        {147, "147"}, {148, "148"}, {149, "149"}, {150, "150"}, {151, "151"}, {152, "152"}, {153, "153"},
        {154, "154"}, {155, "155"}, {156, "156"}, {157, "157"}, {158, "158"}, {159, "159"}, {160, "160"},
        {161, "161"}, {162, "162"}, {163, "163"}, {164, "164"}, {165, "165"}, {166, "166"}, {167, "167"},
        {168, "168"}, {169, "169"}, {170, "170"}, {171, "171"}, {172, "172"}, {173, "173"}, {174, "174"},
        {175, "175"}, {176, "176"}, {177, "177"}, {178, "178"}, {179, "179"}, {180, "180"}, {181, "181"},
        {182, "182"}, {183, "183"}, {184, "184"}, {185, "185"}, {186, "186"}, {187, "187"}, {188, "188"},
        {189, "189"}, {190, "190"}, {191, "191"}, {192, "192"}, {193, "193"}, {194, "194"}, {195, "195"},
        {196, "196"}, {197, "197"}, {198, "198"}, {199, "199"}, {200, "200"}, {201, "201"}, {202, "202"},
        {203, "203"}, {204, "204"}, {205, "205"}, {206, "206"}, {207, "207"}, {208, "208"}, {209, "209"},
        {210, "210"}, {211, "211"}, {212, "212"}, {213, "213"}, {214, "214"}, {215, "215"}, {216, "216"},
        {217, "217"}, {218, "218"}, {219, "219"}, {220, "220"}, {221, "221"}, {222, "222"}, {223, "223"},
        {224, "224"}, {225, "225"}, {226, "226"}, {227, "227"}, {228, "228"}, {229, "229"}, {230, "230"},
        {231, "231"}, {232, "232"}, {233, "233"}, {234, "234"}, {235, "235"}, {236, "236"}, {237, "237"},
        {238, "238"}, {239, "239"}, {240, "240"}, {241, "241"}, {242, "242"}, {243, "243"}, {244, "244"},
        {245, "245"}, {246, "246"}, {247, "247"}, {248, "248"}, {249, "249"}, {250, "250"}, {251, "251"},
        {252, "252"}, {253, "253"}, {254, "254"}, {255, "255"},
    };

    public static (int r, int g, int b) ToRGB(this ConsoleColor color)
    {
        return associations[color];
    }

    public static string Colorize(this char ogChar, int r, int g, int b)
    {
        //\x1b[0m
        return $"\x1b[38;2;{r:000};{g:000};{b:000}m{ogChar}\033[0m";
    }

    public static char[] GetEmptyColorCharBuffer(int charCount)
    {
        return new char[GetColorBufferLength(charCount)];
    }

    public static int GetColorBufferLength(int charCount = 1)
    {
        return 43 * charCount;
    }

    public static void SetRGBBuffer(
        ConColor foreColor,
        ConColor bgColor,
        char chr,
        ref char[] rgbBuffer,
        int offset)
    {
        int backLayer = 48;
        int foreLayer = 38;

        string fr = intLookupStringTable[Int32.Abs(foreColor.r)];
        string fg = intLookupStringTable[Int32.Abs(foreColor.g)];
        string fb = intLookupStringTable[Int32.Abs(foreColor.b)];

        string br = intLookupStringTable[Int32.Abs(bgColor.r)];
        string bg = intLookupStringTable[Int32.Abs(bgColor.g)];
        string bb = intLookupStringTable[Int32.Abs(bgColor.b)];

        string bl = intLookupStringTable[Int32.Abs(backLayer)];
        string fl = intLookupStringTable[Int32.Abs(foreLayer)];

        int index = offset;

        rgbBuffer[index++] = '\x1b'; //Start char
        rgbBuffer[index++] = '['; //[
        rgbBuffer[index++] = fl[1]; //Layer [1]
        rgbBuffer[index++] = fl[2]; //Layer [2]

        rgbBuffer[index++] = ';';
        rgbBuffer[index++] = '2';
        rgbBuffer[index++] = ';';

        //RED
        rgbBuffer[index++] = fr[0];
        rgbBuffer[index++] = fr[1];
        rgbBuffer[index++] = fr[2];

        rgbBuffer[index++] = ';';

        //GREEN
        rgbBuffer[index++] = fg[0];
        rgbBuffer[index++] = fg[1];
        rgbBuffer[index++] = fg[2];

        rgbBuffer[index++] = ';';

        //BLUE
        rgbBuffer[index++] = fb[0];
        rgbBuffer[index++] = fb[1];
        rgbBuffer[index++] = fb[2];

        rgbBuffer[index++] = 'm';

        //BACK GROUND

        rgbBuffer[index++] = '\x1b';

        rgbBuffer[index++] = '[';
        rgbBuffer[index++] = bl[1];
        rgbBuffer[index++] = bl[2];

        rgbBuffer[index++] = ';';
        rgbBuffer[index++] = '2';
        rgbBuffer[index++] = ';';

        //RED
        rgbBuffer[index++] = br[0];
        rgbBuffer[index++] = br[1];
        rgbBuffer[index++] = br[2];

        rgbBuffer[index++] = ';';

        //GREEN
        rgbBuffer[index++] = bg[0];
        rgbBuffer[index++] = bg[1];
        rgbBuffer[index++] = bg[2];

        rgbBuffer[index++] = ';';

        //BLUE
        rgbBuffer[index++] = bb[0];
        rgbBuffer[index++] = bb[1];
        rgbBuffer[index++] = bb[2];

        rgbBuffer[index++] = 'm';

        rgbBuffer[index++] = chr;

        rgbBuffer[index++] = '\x1b';
        rgbBuffer[index++] = '[';
        rgbBuffer[index++] = '0';
        rgbBuffer[index++] = 'm';
    }
}