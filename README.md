תרגיל סודוקו מסכם מאת עומרי גוילי

תהליך הפרויקט

תהליך עשיית פרויקט הסודוקו שלי נעשה בשני שלבים: תכנון ותכנות.
לפני שהתחלתי את התכנות של פרויקט הסודוקו, עבדתי יותר על דרך לעצב את הפרויקט. 
כאשר עבדתי לפני חודשיים על המחשבון, התחלתי ישר לתכנת את האלגוריתם שפותר ביטויים חשבוניים, מבלי לתכנן איך ליצור את הקוד באופן יעיל וגנרי. רק באמצע התהליך הבנתי שהקוד שלי מסורבל ושהייתי צריך להוסיף לו הרבה דברים וזה החזיר אותי המון צעדים אחורה בתהליך.  
בגלל סיבה זו יצא שתכננתי את הפרויקט שלי של הסודוקו במשך הרבה מאוד זמן. רק שבוע וחצי לפני ההגשה של הסודוקו התחלתי לרשום את האלגוריתם.
במהלך כתיבת הקוד, יצרתי לי את כל המחלקות והממשקים שיעזרו לי לפתור את הסודוקו בדרך שרציתי. דרך הפתרון שלי של הסודוקו הייתה פעולה רקורסיבית עיקרית אשר מקבלת מחלקה של לוח ומנסה קודם כל לפתור כמו בן-אדם, לפי שתי טכניקות לפתירת סודוקו נפוצות (hidden singles, naked singles). כאשר המחשב מפסיק לפתור את הלוח כמו בן-אדם, הוא שם במשבצת הריקה הראשונה על הלוח ניחוש תקין ובודק אם ניחוש זה תקין לפי בדיקה רקורסיבית של אותו לוח. כאשר יוצא שלמשבצת מסוימת אין ניחוש חוקי, אז ניתן להבין שניחוש קודם היה ניחוש לא טוב אז הפעולה חוזרת אחורה באמצעות backtracking ומנסה לפתור את הלוח לפי הניחוש הנוסף במשבצת הריקה. 
אם יוצא שללוח אין יותר משבצות ריקות, אז הלוח נפתר, ותירשם הודעה בהתאם. אך אם אין יותר חזרות רקורסיביות בפעולה ולמשבצת הראשונה הריקה אין עוד אפשרויות ניחוש, אז הלוח לא נפתר ותישלח הודעה בהתאם.
זה תהליך עשיית הפרויקט שלי לסודוקו וכיצד ניגשתי אליו

מה פרויקט הסודוקו שלי מכיל

על מנת להפוך את הקוד שלי לפתירת סודוקו ליותר יעיל, נקי ומהיר, הוספתי לפרויקט כמה ממשקים ומחלקות. להלן המחלקות העיקריות בפרויקט:
ממשק קריאת סודוקו- לממשק זה שתי פעולות עיקריות: קלט ופלט. בעזרת ממשק זה, המחשב יכול לקבל קלט של מחרוזת סודוקו ולשלוח מחרוזת סודוקו פתור למשתמש בחזרה באופן גנרי. שתי הדרכים העיקריות לקריאת הסודוקו הם דרך הקונסולה ודרך קבצים שנמצאים במחשב, לכל דרך של קריאה המחשב מבצע את פעולת הקלט והפלט בהתאם.

חריגות- מחלקה זו היא מחלקה ששומרת את כל החריגות האפשריות שיכול להיות בסודוקו, למשל: קלט מחרוזת של לוח סודוקו המכיל סמלים לא חוקיים, קלט לוח סודוקו לא מרובע וכו'.. 

מחלקת משבצת- מחלקה זו היא מחלקה קטנה שמכילה רק 3 תכונות, המספר שעל המשבצת, רשימת האופציות שיכול להיות למשבצת ומיקום המשבצת על המערך משבצות. מטרת מחלקת המשבצת הינה בשביל שיהיה ניתן לגשת כל התאים שעל הלוח סודוקו שיישמר לנו מידע על כל משבצת באופן יעיל

מחלקת קבוצה- מחלקה זו היא מחלקה המייצגת קבוצה על הלוח, קבוצה יכולה להיות שלושה דברים: שורה, עמודה וקופסא. המטרה למחלקה זו הייתה ההבנה שיש בעצם תמיד כמות שווה של שורות, עמודות וקופסאות בלוח סודוקו תקין לכן ניתן בעצם לסרוק את הלוח לפי קבוצות מסוימים ובכך לא לחזור על אותו קוד ולהפוך את כל הבדיקות שיש בפעולות במחלקה של הלוח ליעילות יותר ופחות מסורבלות. 

מחלקת לוח- מחלקה זו היא המחלקה העיקרית בפרויקט הסודוקו שלי. המחלקה מכילה: מערך דו מימדי של משבצות, מערך של המספרים שיכולים להירשם על הלוח, רשימת של תאים ריקים במערך רשימה של תאים שהשתנו במערך, מערך שורות, מערך עמודות ומערך קופסאות. בעזרת כל התכונות האלו, ניתן היה ליצור פעולות אשר עוזרות בפתירת סודוקו באופן יעיל ונקי שיכול לפתור לוחות סודוקו בפחות מ-9 אלפיות השנייה.
