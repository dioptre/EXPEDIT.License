﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXPEDIT.License.Helpers
{
    public class ConstantsHelper
    {
        internal static string[] WIFS_PRIVATE_CREDNEXUS = new string[] {
            "732vd1BJ2jQT1skxAzdKNRGnNpvmCTDLskD29E89ipKA7b31UdQ", //NEXUSCK711W5xQ3Pw5HBbiWTUtJLmbvGXz
            "74b5qwCmcitjRRfthEYHZHiR1aGcJPsjqDfM1UqizEAiJx4dS5N", //NEXUSCAVfEk8nLKkWGBGdERth5sKuqNBjQ
            "73xQqDGaqSdZC1Vn3PY1LFK1Ae8tBRwbdbF4Z4AKVa8W6zWmuDM"  //NEXUSCvftgFVJVknYYZk8MZkb4AaANWhnx
        };

        internal const string WIF_SITE = "5JxoMpwRD2C3WUkCTrMM2xGB1dA7CghzFfjBESm5FmjDPUa7rS6"; //MzJwgvBLtrEeoNUzBPQb57SGp2AVf8MstF
        internal const string KEY_PRIVATE_DEFAULT = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQCYU4xT4bwGV9y08lLj4MOdPy2MZTTlGFiPloR1kK8z4nwtGFIxqy2DBEAuis6P9Z+Tk0S+/rBtX5QgpIPqY98SeYhCkPeTucwqTru7HG7Tdj9NZlPlnQ258gelEHSO18L7mWk+JYl6f6u8kFCaDWwG+KlV5WWXExQLA5dXqjrX+WCEE8UZ5eb7zwFZvenAgGgnmH2WyJC3qOlrhdHbPvTBoCbG3KAouV/cUn24a9TTQCF3DHTi4FPK+hyKHctG73dfQm5pJBHKDdqJbQ0Va0c5dDtu5AojGzBSnMImKNsPKr63LWuO64FMKGl11FaNZe1mIehMa4jsY1ffpyXSLYunAgMBAAECggEAZAx0HeAlJDFvWDXVNbE6Kj0FyLHspRBxkpX1GFbYjIaUsvXHfrIE6YnQMgGfnLRihIZ039HexWfCnhIQRtIkATlrwvT+d7vQGnWuHj6VmDSRbV/peOXHzzrlxIfjVrLmcWSY2GXFP309qlNLbXOlYYrPhghuymSQhI9uRvkbPyCacVFA/gWi8CsdK7ip0YrM/wOLM8sI5+lM9+kQueooYwSDz3EHCrbPCd7r+3qKk5PHNNECeCBytT/jw3j6ZIzLvjNGhrdQGMNhRxkB3C0hFJFhqq4xI6yOs6Q/XztUwYv+4lgS4K2jeEip4fQJW7rQiSftr5bAM9Fp9oHuZFrOsQKBgQDbH+CuSlAsqOpdqgxAu7+leMoIVS3Jujw/iTwGsdjYA8a0zqg18kHS8heDs7tZtbxuz44bmfTcVgCDRYOUamwrbmoAHhtmP9eLYGGGlKqQbeOV48qPywVZFChGO3QlmZ45CtcTKvTuZgxTR6aKwQ0xXOoAa0In7u3JuHTtZjstSQKBgQCx9e7iUA9U75148DeDGPtck0qNZ/aKGKHJCNIEkh52uoaEftuT5g+U852KoS69sz2cUx/WzpdLxScHn44yGC/FgkSQzJcMd8vsn4rl8Yx1uNWXhnkwNM7VjD1VwZ413IDkh7DEI3fNEcwqk2nowDsjrFfX8ZjnOO8i8rxpYLqhbwKBgQCKImRfOxWjsbBc72/d9v1vcN/btOayfqawXvDqP381XdwL6yL7Lwbz1g2gxtLaUMjDCjDJkZpctBKKrm2uSBB8qJRGErSvFpvojw+r6VhEyCFqQjlVwGRUrXJeI+iqM1cdGopO2Quipc4rScXhPqX0cmBJd1QzHFnmilObvJCdkQKBgD/AsQGWWMe+x5UpyVlHu9TgV1btJZ83T84rQMGubwdtrv8MSzFiu7ZKx+d/8rS2351/EersO7tDN8Y9XL2JeKOzFUkiYgJvcDimtyXFMOKDgtEztXqVkHtkMBzmrfzxr6MvER5S7noipBekk85z/zu6ZAXSYUqEVPcaKnE92941AoGBALgr71ngp0O3+91K+KOnUDrKgoVtXmU9s2WMQkrNAOvvDQuv2j8zwA4rx90tCJ2J6udEuaLpuQDYwjyf2Dwl+VfAKDy4fAkctWQvJv+agSUTIw4427YbxWcHp+FRnscxoVf+j5pI10q/uW1OH4y/rB0yIlNqah0eB9y+fz2RQhs6";

        public static string DOCUMENT_TYPE_SOFTWARE_SUBMISSION = "Software Submission";
        public static string DOCUMENT_TYPE_INVOICE = "Invoice";

        public static Guid COMPANY_DEFAULT = new Guid("6887ABC9-E2D8-4A2D-B143-6C3E5245C565");
        public static Guid ACCOUNT_TYPE_ONLINE = new Guid("5C329B8D-007D-435E-8261-4FA72D7DF28A");
        public static Guid DEVICE_TYPE_SOFTWARE = new Guid("3f526009-827a-41b0-a633-14b422bdf27f");
        public static Guid ROUTE_TYPE_STORE_INTERNAL = new Guid("1a01fc89-c014-433f-be04-39c2f956aeb2");
        public static Guid ROUTE_TYPE_STORE_EXTERNAL = new Guid("7c9f3a25-011b-4f5e-8b1e-d345da13f8b1");
        public static Guid UNIT_SI_SECONDS = new Guid("5AF72C77-A76E-4234-A16E-3F7898799EEA");
        public static Guid CONTRACT_PARTNER = new Guid("e8ed2f94-1100-43a2-90cd-206d228090e2");
        public static int SQL_MAX_INT = 2147483647;
        public static string STAT_NAME_DOWNLOADS = "Downloads";
        public static string STAT_NAME_ROUTES = "Routes";
        public static string STAT_NAME_CLICKS_BUY = "ClicksBuy";
        public static string STAT_NAME_REFERRAL = "Referral";
        public static string STAT_NAME_CLICKS_CONFIRM = "ClicksConfirm";
        public static string METADATA_ANTIFORGERY = "E_ANTIFORGERY";
        public static string REFERENCE_TYPE_LABOUR = "E_LABOUR";
        public static string REFERENCE_TYPE_CONTRACT = "X_Contract";
        public const decimal GST_AU = 0.1m;
        public static decimal TAX_DEFAULT = GST_AU;
        public static string LICENSE_SERVER_AUTH_METHOD = "Simple";
        public static int DOWNLOADS_REMAINING_DEFAULT = 10;
        public static Guid FILE_TYPE_EXTERNAL= new Guid("a7d379b3-b4fe-40a1-bebd-19e3d61f3477");
        public static Guid FILE_TYPE_USER_GUIDE= new Guid("5f14ae60-6ca3-46e7-973e-1fb45e7b7362");
        public static Guid FILE_TYPE_SOFTWARE= new Guid("b2df1ccc-cd61-4d80-859a-40dd06b10e63");
        public static Guid FILE_TYPE_GLOBAL= new Guid("3e3ebe72-3a3d-4ad5-946b-52befb8b483f");
        public static Guid FILE_TYPE_INVOICE= new Guid("26c6a363-1a75-4530-ad44-cd18a47b69f1");
        public static Guid FILE_TYPE_INTERNAL= new Guid("b8491f60-8ab2-444d-8d88-e0a34cddeafe");        
        public static string ADDRESS_APP_OWNER =
                "EXPEDIT SOLUTIONS PTY LTD - MiningAppstore\r\n" +
                "ABN 93152456374\r\n" +
                "3 Fincastle Street, Moorooka, Brisbane\r\n" +
                "QLD, 4105 Australia\r\n\r\n" +
                "P: +61733460727\r\n" +
                "E: accounts@miningappstore.com\r\n" +
                "U: http://miningappstore.com";
        public static string APP_OWNER = "MINING APPSTORE";
        public static string PDF_LOGO = @"EXPEDIT.License\Images\pdfheader.jpg";        

    }
}