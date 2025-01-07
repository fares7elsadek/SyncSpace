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

            body,
            table,
            td,
            a {{
                -ms-text-size-adjust: 100%;
                -webkit-text-size-adjust: 100%;
            }}

            table,
            td {{
                mso-table-rspace: 0pt;
                mso-table-lspace: 0pt;
            }}

            img {{
                -ms-interpolation-mode: bicubic;
            }}

            a[x-apple-data-detectors] {{
                font-family: inherit !important;
                font-size: inherit !important;
                font-weight: inherit !important;
                line-height: inherit !important;
                color: inherit !important;
                text-decoration: none !important;
            }}

            body {{
                width: 100% !important;
                height: 100% !important;
                padding: 0 !important;
                margin: 0 !important;
                background: linear-gradient(to right, #FF20DE 20%, #18b7bd 50%);
            }}

            table {{
                border-collapse: collapse !important;
            }}

            a {{
                color: #ffffff;
            }}

            img {{
                height: auto;
                line-height: 100%;
                text-decoration: none;
                border: 0;
                outline: none;
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

            .email-button {{
                margin-top: 30px;
                text-align: center;
            }}

            .email-button a {{
                display: inline-block;
                background: linear-gradient(to right, #FF20DE, #18b7bd);
                color: #ffffff;
                padding: 12px 30px;
                font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
                font-size: 16px;
                font-weight: 700;
                border-radius: 6px;
                text-decoration: none;
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

            .email-footer a {{
                color: #ffffff;
                text-decoration: none;
            }}

            .video-icon {{
                text-align: center;
                margin: 30px 0;
            }}

            .video-icon img {{
                width: 60px;
            }}

            .divider {{
                border-top: 1px solid #e2e8f0;
                margin: 20px 0;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <!-- Header -->
            <div class='email-header'>
                <img
                    src='https://res.cloudinary.com/df6ylojjq/image/upload/v1736218064/SyncSpace_1_deheip.png'
                    alt='SyncSpace Logo'
                />
            </div>

            <!-- Body -->
            <div class='email-body'>
                <h1>Welcome to SyncSpace!</h1>
                <p>
                    Confirm your email address to unlock seamless video streaming with your friends. Enjoy synchronized
                    viewing and real-time interactions.
                </p>
                <div class='video-icon'>
                    <img
                        src='https://img.icons8.com/ios-filled/100/FF20DE/video.png'
                        alt='Video Icon'
                    />
                </div>
                <div class='email-button'>
                    <a href='{Url}' target='_blank'>Confirm Email</a>
                </div>
                <div class='divider'></div>
                <p>
                    If the button doesn't work, copy and paste the following link in your browser:
                </p>
                <p>
                    <a href='{Url}' target='_blank'>{Url}</a>
                </p>
            </div>

            <!-- Footer -->
            <div class='email-footer'>
                <p>
                    You received this email because you signed up for SyncSpace. If you didn't request this, please
                    ignore this email.
                </p>
                <p>© 2024 SyncSpace. All Rights Reserved.</p>
            </div>
        </div>
    </body>
</html>
";



    }
}
