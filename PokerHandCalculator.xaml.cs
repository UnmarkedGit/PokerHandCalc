using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


namespace Poker_Hands_Mark_Kerschner
{
    public sealed partial class MainPage : Page
    {
        //^^^================================================================================^^^
        //
        //      This is just auto made helps with the image initialization.
        //VVV================================================================================VVV
        public MainPage()
        {
            this.InitializeComponent();
            _1stHandPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _2ndHandPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _1stPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _2ndPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _3rdPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _4thPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _5thPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
        }
        //^^^================================================================================^^^
        //
        //      These are just some feilds.
        //VVV================================================================================VVV
        int Heart = 0;
        int Spade = 0;
        int Club = 0;
        int Diamond = 0;
        int Rank;
        int[] dealtCardFace = new int[7];
        string[] dealtCardSuit = new string[7];
        string[] dealtCards = new string[7];
        //^^^================================================================================^^^
        //
        //      The Buttons for the form:
        //
        //          >RandomSelectBtn_Click: Randomly selects cards for the textboxes.
        //
        //          >StartBtn_Click: Works pretty damn fine! I tested it some and I didnt see
        //                           any notiable errors other than needing to double click 
        //                           it.
        //
        //              -If the textboxes are empty will fill.
        //
        //              -Gets all textbox info into array, splits array into two arrays(Face,
        //               Suit).
        //
        //              -The Chunky part(RankTypeCompile();), applies the (Face,Suit) arrays 
        //               too 12 bool methodes, and "If, Else if, else" statements that totals
        //               up to 33!
        //
        //              -Rating, Winnable, Fold are determined by the Rank int.
        //
        //              -Displays cards and results.
        //
        //          >ClearBtn_Click: Wipes the input/result clean, and the Card.png
        //                           to back.jpg.
        //VVV================================================================================VVV
        private void RandomSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            var theCardDeck = new CardDeck();
            theCardDeck.Shuffle();
            
            _1stHC.Text = $"{theCardDeck.Deal(),-1}";
            _2ndHC.Text = $"{theCardDeck.Deal(),-1}";
            _1stTC.Text = $"{theCardDeck.Deal(),-1}";
            _2ndTC.Text = $"{theCardDeck.Deal(),-1}";
            _3rdTC.Text = $"{theCardDeck.Deal(),-1}";
            _4thTC.Text = $"{theCardDeck.Deal(),-1}";
            _5thTC.Text = $"{theCardDeck.Deal(),-1}";
        }
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            autoFill();
            SplitFaceSuit();
            RankTypeCompile();
            RTResult.Text = $"{RankTypeCompile()}";
            RatingResult.Text = $"{RateCompile()}";
            WinnableResult.Text = $"{WinCompile()}";
            FoldResult.Text = $"{FoldCompile()}";
            _1stHandPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[0]}.png"));
            _2ndHandPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[1]}.png"));
            _1stPickPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[2]}.png"));
            _2ndPickPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[3]}.png"));
            _3rdPickPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[4]}.png"));
            _4thPickPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[5]}.png"));
            _5thPickPic.Source = new BitmapImage(new Uri(BaseUri, $@"/images/{dealtCards[6]}.png"));
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            _1stHC.Text = "";
            _2ndHC.Text = "";
            _1stTC.Text = "";
            _2ndTC.Text = "";
            _3rdTC.Text = "";
            _4thTC.Text = "";
            _5thTC.Text = "";
            RTResult.Text = "";
            RatingResult.Text = "";
            WinnableResult.Text = "";
            FoldResult.Text = "";
            _1stHandPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _2ndHandPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _1stPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _2ndPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _3rdPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _4thPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));
            _5thPickPic.Source = new BitmapImage(new Uri(BaseUri, @"/images/back.jpg"));

        }
        //^^^================================================================================^^^
        //
        //      This just fills it if empty.
        //VVV================================================================================VVV
        private void autoFill()
        {
            var theCardDeck = new CardDeck();
            theCardDeck.Shuffle();
            if (_1stHC.Text == "")
            {
                _1stHC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_2ndHC.Text == "")
            {
                _2ndHC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_1stTC.Text == "")
            {
                _1stTC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_2ndTC.Text == "")
            {
                _2ndTC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_3rdTC.Text == "")
            {
                _3rdTC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_4thTC.Text == "")
            {
                _4thTC.Text = $"{theCardDeck.Deal(),-1}";
            }
            if(_5thTC.Text == "")
            {
                _5thTC.Text = $"{theCardDeck.Deal(),-1}";
            }
        }
        //^^^================================================================================^^^
        //
        //      I like this, its useful and kept it easy to 
        //      get the info i needed from the Hand/Table Cards.
        //VVV================================================================================VVV
        public void SplitFaceSuit()
        {
            dealtCards[0] = _1stHC.Text;
            dealtCards[1] = _2ndHC.Text;
            dealtCards[2] = _1stTC.Text;
            dealtCards[3] = _2ndTC.Text;
            dealtCards[4] = _3rdTC.Text;
            dealtCards[5] = _4thTC.Text;
            dealtCards[6] = _5thTC.Text;
            
           

            for (int s = 0; s < 7; s++)
            {
                
                string y = dealtCards[s];
                dealtCardSuit[s] = y[dealtCards[s].Length - 1].ToString();
                dealtCardFace[s] = Convert.ToInt32(Regex.Replace(dealtCards[s], "[^0-9.]", ""));
            }
        }
        //^^^================================================================================^^^
        //
        //      The #1 cause of frustraition & procrastination used a little bit of the 
        //      internet. I was hoping to add more but this took most of the fuel.
        //VVV================================================================================VVV
        private string RankTypeCompile()
        {
            Array.Sort(dealtCardFace);
            Array.Sort(dealtCardSuit);
            if ((dealtCardFace[6] == 13 && dealtCardFace[0] == 1 && dealtCardFace[5] == 12 && 
                dealtCardFace[4] == 11 && dealtCardFace[3] == 10) && isFlush() == true)
            {
                Rank = 1;
                return "Rank: 1st Type: Royal Flush!";
            }
            else if (isStraight() && isFlush() == true)
            {
                Rank = 2;
                return "Rank: 2nd Type: Straight Flush!";
            }
            else if (isFourOfKind() == true)
            {
                Rank = 3;
                return "Rank: 3rd Type: 4 of a Kind!";
            }
            else if (isFullhouse() == true)
            {
                Rank = 4;
                return "Rank: 4th Type: Full House!";
            }
            else if (isFlush() == true)
            {
                Rank = 5;
                return "Rank: 5th Type: Flush!";
            }
            else if (isStraight() == true)
            {
                Rank = 6;
                return "Rank: 6th Type: Straight.";
            }
            else if (isThreeOfKind() == true)
            {
                Rank = 7;
                return "Rank: 7th Type: 3 of a Kind.";
            }
            else if (isTwoPair() == true)
            {
                Rank = 8;
                return "Rank: 8th Type: 2 Pair.";
            }
            else if (isPair() == true)
            {
                Rank = 9;
                return "Rank: 9th Type: Pair.";
            }
            else
            {
                Rank = 10;
                return "Rank: 10th Type: High card. :(";
            }
        }

        private bool isStraight()
        {
            if ((dealtCardFace[6] == 13 && dealtCardFace[0] == 1 &&
                dealtCardFace[5] == 12 && dealtCardFace[4] == 11 &&
                dealtCardFace[3] == 10) || (dealtCardFace[0] + 1 == dealtCardFace[1] && dealtCardFace[1] + 1 == dealtCardFace[2] &&
                dealtCardFace[2] + 1 == dealtCardFace[3] && dealtCardFace[3] + 1 == dealtCardFace[4]) ||
                (dealtCardFace[1] + 1 == dealtCardFace[2] && dealtCardFace[2] + 1 == dealtCardFace[3] &&
                dealtCardFace[3] + 1 == dealtCardFace[4] && dealtCardFace[4] + 1 == dealtCardFace[5]) ||
                (dealtCardFace[2] + 1 == dealtCardFace[3] && dealtCardFace[3] + 1 == dealtCardFace[4] &&
                dealtCardFace[4] + 1 == dealtCardFace[5] && dealtCardFace[5] + 1 == dealtCardFace[6]) )return true;
            return false;
        }

        private bool isFullhouse()
        {
            if ((dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[0] == dealtCardFace[2] && dealtCardFace[3] == dealtCardFace[4]) ||
                dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[2] == dealtCardFace[4]) return true;
            if ((dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[1] == dealtCardFace[3] && dealtCardFace[4] == dealtCardFace[5]) ||
                dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[3] == dealtCardFace[4] && dealtCardFace[3] == dealtCardFace[5]) return true;
            if ((dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[2] == dealtCardFace[4] && dealtCardFace[5] == dealtCardFace[6]) ||
                dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[4] == dealtCardFace[5] && dealtCardFace[4] == dealtCardFace[6]) return true;
            return false;
        }

        private bool isFlush()
        {
             Heart = 0;
             Spade = 0;
             Club = 0;
             Diamond = 0;
            for (int i = 0; i < 7; i++)
            {
                if (dealtCardSuit[i] == "H")
                {
                    Heart++;
                }
                else if (dealtCardSuit[i] == "S")
                {
                    Spade++;
                }
                else if (dealtCardSuit[i] == "C")
                {
                    Club++;
                }
                else if (dealtCardSuit[i] == "D")
                {
                    Diamond++;
                }
                
            }
            if (Club == 5 || Heart == 5 || Spade == 5 || Diamond == 5
                || Club == 6 || Heart == 6 || Spade == 6 || Diamond == 6
                || Club == 7 || Heart == 7 || Spade == 7 || Diamond == 7)
            {
                return true;
            }
            return false;
        }

        private bool isFourOfKind()
        {
            if ((dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[0] == dealtCardFace[2] && dealtCardFace[0] == dealtCardFace[3]) ||
                (dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[1] == dealtCardFace[3] && dealtCardFace[1] == dealtCardFace[4]) ||
                (dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[2] == dealtCardFace[4] && dealtCardFace[2] == dealtCardFace[5]) ||
                (dealtCardFace[3] == dealtCardFace[4] && dealtCardFace[3] == dealtCardFace[5] && dealtCardFace[3] == dealtCardFace[6])) return true;
            return false;
        }

        private bool isThreeOfKind()
        {
            if ((dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[0] == dealtCardFace[2]) ||
                (dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[1] == dealtCardFace[3]) ||
                (dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[2] == dealtCardFace[4]) ||
                (dealtCardFace[3] == dealtCardFace[4] && dealtCardFace[3] == dealtCardFace[5]) ||
                (dealtCardFace[4] == dealtCardFace[5] && dealtCardFace[4] == dealtCardFace[6])) return true;
            return false;
        }

        private bool isTwoPair()
        {
            if ((dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[2] == dealtCardFace[3]) ||
                (dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[3] == dealtCardFace[4]) ||
                (dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[4] == dealtCardFace[5]) ||
                (dealtCardFace[0] == dealtCardFace[1] && dealtCardFace[5] == dealtCardFace[6]) ||
                (dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[3] == dealtCardFace[4]) ||
                (dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[4] == dealtCardFace[5]) ||
                (dealtCardFace[1] == dealtCardFace[2] && dealtCardFace[5] == dealtCardFace[6]) ||
                (dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[4] == dealtCardFace[5]) ||
                (dealtCardFace[2] == dealtCardFace[3] && dealtCardFace[5] == dealtCardFace[6]) ||
                (dealtCardFace[3] == dealtCardFace[4] && dealtCardFace[5] == dealtCardFace[6]) ||
                (dealtCardFace[4] == dealtCardFace[5] && dealtCardFace[4] == dealtCardFace[6])) return true;
            return false;
        }

        private bool isPair()
        {
            if (dealtCardFace[0] == dealtCardFace[1] || dealtCardFace[2] == dealtCardFace[3] ||
                dealtCardFace[0] == dealtCardFace[1] || dealtCardFace[3] == dealtCardFace[4] ||
                dealtCardFace[0] == dealtCardFace[1] || dealtCardFace[4] == dealtCardFace[5] ||
                dealtCardFace[0] == dealtCardFace[1] || dealtCardFace[5] == dealtCardFace[6]) return true;
            return false;
        }
        //^^^================================================================================^^^
        //
        //      rankings
        //VVV================================================================================VVV
        private string RateCompile()
        {
            if (Rank < 3) return "*****";
            if (Rank < 5) return "****";
            if (Rank < 7) return "***";
            if (Rank <= 9) return "**";
            return "*";
        }
        private string WinCompile()
        {
            if (Rank <= 5) return "Very Likely!";
            if (Rank <= 9) return "Likely!";
            return "Not Likely!";
        }
        private string FoldCompile()
        {
            if (Rank < 9) return "No!";
            if (Rank == 9) return "Depends on behavior!";
            return "Yes!";
        }
    }
    //^^^================================================================================^^^
    //
    //      This was mostly(all) taken out of the book and helps a ton with 
    //      selecting cards and what i needed in the PB project.
    //VVV================================================================================VVV
    class CardDeck
    {
        Random rand = new Random();
        private const int totalNumCards = 52;
        private Card[] deck = new Card[totalNumCards];
        private int currCard = 0;

        public CardDeck()
        {
            int[] Faces = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
            string[] Suits = { "H", "D", "C", "S" };
            for (var count = 0; count < deck.Length; ++count)
            {
                deck[count] = new Card(Faces[count % 13], Suits[count / 13]);
            }
        }

        public void Shuffle()
        {
            for (int f = 0; f < totalNumCards; f++)
            {
                int s = rand.Next(totalNumCards);

                Card temp = deck[f];
                deck[f] = deck[s];
                deck[s] = temp;
            }
        }

        public Card Deal()
        {
            if (currCard < deck.Length)
            {
                return deck[currCard++];
            }
            else
            {
                return null;
            }
        }

    }

    class Card
    {
        private int Face { get; }
        private string Suit { get; }
        
        public Card(int face, string suit)
        {
            Face = face;
            Suit = suit;
        }
        public override string ToString() => $"{Face}{Suit}";
    }
    //^^^================================================================================^^^


}
