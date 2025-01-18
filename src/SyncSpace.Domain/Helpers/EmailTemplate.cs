using System;

namespace SyncSpace.Domain.Helpers;

public static class EmailTemplate
{
    public  static string ConfirmEmail(string Url)
    {
        return $@"
<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8' />
        <meta http-equiv='x-ua-compatible' content='ie=edge' />
        <title>Email Confirmation</title>
        <meta name='viewport' content='width=device-width, initial-scale=1' />
        <style type='text/css'>
            @media screen {{
                @font-face {{
                    font-family: 'Inter';
                    font-style: normal;
                    font-weight: 400;
                    src: url('https://fonts.gstatic.com/s/inter/v7/UcCO3FwrJCnkr2w6DGsTTc7Xnff34v9p.woff2') format('woff2');
                }}
                @font-face {{
                    font-family: 'Inter';
                    font-style: normal;
                    font-weight: 700;
                    src: url('https://fonts.gstatic.com/s/inter/v7/UcCO3FwrJCnkr2w6DGsTTc7Xnff34v9p.woff2') format('woff2');
                }}
            }}

            body {{
                margin: 0;
                padding: 0;
                width: 100% !important;
                height: 100% !important;
                background-color: #1a1a2e;
                font-family: 'Inter', Helvetica, Arial, sans-serif;
                color: #ffffff;
            }}

            table {{
                border-collapse: collapse !important;
                width: 100%;
            }}

            .email-container {{
                max-width: 600px;
                margin: 0 auto;
                background-color: #1a1a2e;
                border-radius: 8px;
                overflow: hidden;
                box-shadow: 0 4px 12px rgba(0, 0, 0, 0.5);
            }}

            .email-header {{
                background: #27293d;
                text-align: center;
                padding: 40px;
                border-bottom: 3px solid #0079ff;
            }}

            .email-header img {{
                width: 120px;
            }}

            .email-body {{
                padding: 30px;
            }}

            .email-body h1 {{
                font-size: 24px;
                font-weight: 700;
                text-align: center;
                margin-bottom: 20px;
                color: #ffffff;
            }}

            .email-body p {{
                font-size: 16px;
                line-height: 24px;
                text-align: center;
                color: #dcdcdc;
            }}

            .email-button {{
                text-align: center;
                margin: 30px 0;
            }}

            .email-button a {{
                display: inline-block;
                background: linear-gradient(90deg, #0079ff, #00c6ff);
                color: #ffffff;
                padding: 14px 40px;
                font-size: 16px;
                font-weight: 700;
                text-decoration: none;
                border-radius: 8px;
                transition: background 0.3s ease;
            }}

            .email-button a:hover {{
                background: linear-gradient(90deg, #00c6ff, #0079ff);
            }}

            .divider {{
                border-top: 1px solid #393e56;
                margin: 20px 0;
            }}

            .email-footer {{
                background: #27293d;
                text-align: center;
                padding: 20px;
                font-size: 14px;
                color: #a5a5a5;
            }}

            .email-footer p {{
                margin: 5px 0;
            }}

            .email-footer a {{
                color: #00c6ff;
                text-decoration: none;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <!-- Header -->
            <div class='email-header'>
                <img
                    src='https://res.cloudinary.com/df6ylojjq/image/upload/v1737147779/logo_zlauub.png'
                    alt='SyncSpace Logo'
                />
            </div>

            <!-- Body -->
            <div class='email-body'>
                <h1>Welcome to SyncSpace!</h1>
                <p>
                    Confirm your email to start streaming content with your friends in sync. 
                    Experience seamless viewing and interactive features with SyncSpace.
                </p>
                <div class='email-button'>
                    <a href='{Url}' target='_blank'>Confirm Email</a>
                </div>
                <div class='divider'></div>
                <p>If the button doesn't work, copy and paste the link below:</p>
                <p><a href='{Url}' target='_blank'>{Url}</a></p>
            </div>

            <!-- Footer -->
            <div class='email-footer'>
                <p>You received this email because you signed up for SyncSpace.</p>
                <p>© {DateTime.Now.Year} SyncSpace. All rights reserved.</p>
                <p><a href='#'>Privacy Policy</a> | <a href='#'>Contact Us</a></p>
            </div>
        </div>
    </body>
</html>
";


    }


    public static string PasswordResetEmailCode(string ResetCode)
    {
        return $@"
<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8' />
        <meta http-equiv='x-ua-compatible' content='ie=edge' />
        <title>Password Reset</title>
        <meta name='viewport' content='width=device-width, initial-scale=1' />
        <style type='text/css'>
            @media screen {{
                @font-face {{
                    font-family: 'Source Sans Pro';
                    font-style: normal;
                    font-weight: 400;
                    src: local('Source Sans Pro Regular'),
                        url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff)
                            format('woff');
                }}
                @font-face {{
                    font-family: 'Source Sans Pro';
                    font-style: normal;
                    font-weight: 700;
                    src: local('Source Sans Pro Bold'),
                        url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff)
                            format('woff');
                }}
            }}

            body {{
                width: 100% !important;
                height: 100% !important;
                padding: 0 !important;
                margin: 0 !important;
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
            }}

            .email-container {{
                max-width: 600px;
                margin: 0 auto;
                background: #ffffff;
                border-radius: 8px;
                overflow: hidden;
            }}

            .email-header {{
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
                text-align: center;
                padding: 30px;
            }}

            .email-header img {{
                width: 100px;
            }}

            .email-body {{
                background-color: #ffffff;
                padding: 40px 30px;
            }}

            .email-body h1 {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 26px;
                font-weight: 700;
                text-align: center;
                color: #333333;
                margin-bottom: 20px;
            }}

            .email-body p {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 16px;
                line-height: 24px;
                text-align: center;
                color: #555555;
            }}

            .reset-code {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 20px;
                font-weight: bold;
                text-align: center;
                color: #FF20DE;
                margin: 20px 0;
            }}

            .email-footer {{
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
                padding: 20px;
                text-align: center;
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 14px;
                color: #ffffff;
            }}

            .email-footer p {{
                margin: 5px 0;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <!-- Header -->
            <div class='email-header'>
                <img
                    src='https://res.cloudinary.com/df6ylojjq/image/upload/v1736258888/SyncSpace_ldppb8.png'
                    alt='SyncSpace Logo'
                />
            </div>

            <!-- Body -->
            <div class='email-body'>
                <h1>Password Reset Request</h1>
                <p>
                    Use the code below to reset your password. If you did not request this, please ignore this email.
                </p>
                <div class='reset-code'>{ResetCode}</div>
                <p>
                    This code is valid for a limited time. Please do not share it with anyone.
                </p>
            </div>

            <!-- Footer -->
            <div class='email-footer'>
                <p>
                    You received this email because you requested a password reset. If you didn't request this, please
                    ignore this email.
                </p>
                <p>© {DateTime.Now.Year} SyncSpace. All Rights Reserved.</p>
            </div>
        </div>
    </body>
</html>
";
    }

    public static string PasswordResetEmailLink(string ResetLink)
    {
        return $@"
<!DOCTYPE html>
<html>
    <head>
        <meta charset='utf-8' />
        <meta http-equiv='x-ua-compatible' content='ie=edge' />
        <title>Password Reset</title>
        <meta name='viewport' content='width=device-width, initial-scale=1' />
        <style type='text/css'>
            @media screen {{
                @font-face {{
                    font-family: 'Source Sans Pro';
                    font-style: normal;
                    font-weight: 400;
                    src: local('Source Sans Pro Regular'),
                        url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff)
                            format('woff');
                }}
                @font-face {{
                    font-family: 'Source Sans Pro';
                    font-style: normal;
                    font-weight: 700;
                    src: local('Source Sans Pro Bold'),
                        url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff)
                            format('woff');
                }}
            }}

            body {{
                width: 100% !important;
                height: 100% !important;
                margin: 0 !important;
                padding: 0 !important;
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
            }}

            .email-container {{
                max-width: 600px;
                margin: 20px auto;
                background: #ffffff;
                border-radius: 8px;
                overflow: hidden;
                box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }}

            .email-header {{
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
                text-align: center;
                padding: 30px;
            }}

            .email-header img {{
                width: 120px;
            }}

            .email-body {{
                background-color: #ffffff;
                padding: 40px 30px;
                text-align: center;
            }}

            .email-body h1 {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 26px;
                font-weight: 700;
                color: #333333;
                margin-bottom: 20px;
            }}

            .email-body p {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 16px;
                line-height: 24px;
                color: #555555;
                margin-bottom: 20px;
            }}

            .reset-link {{
                margin: 30px 0;
            }}

            .reset-link a {{
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 16px;
                font-weight: bold;
                color: #ffffff;
                background-color: #18b7bd;
                text-decoration: none;
                padding: 12px 24px;
                border-radius: 5px;
                display: inline-block;
            }}

            .email-footer {{
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
                padding: 20px;
                text-align: center;
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 14px;
                color: #ffffff;
            }}

            .email-footer p {{
                margin: 5px 0;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <!-- Header -->
            <div class='email-header'>
                <img
                    src='https://res.cloudinary.com/df6ylojjq/image/upload/v1736258888/SyncSpace_ldppb8.png'
                    alt='SyncSpace Logo'
                />
            </div>

            <!-- Body -->
            <div class='email-body'>
                <h1>Password Reset Request</h1>
                <p>
                    Click the button below to reset your password. If you did not request this, please ignore this email.
                </p>
                <div class='reset-link'>
                    <a href='{ResetLink}'>Reset Your Password</a>
                </div>
                <p>
                    This link is valid for a limited time. Please do not share it with anyone.
                </p>
            </div>

            <!-- Footer -->
            <div class='email-footer'>
                <p>
                    You received this email because you requested a password reset. If you didn't request this, please
                    ignore this email.
                </p>
                <p>© {DateTime.Now.Year} SyncSpace. All Rights Reserved.</p>
            </div>
        </div>
    </body>
</html>
";
    }


}
