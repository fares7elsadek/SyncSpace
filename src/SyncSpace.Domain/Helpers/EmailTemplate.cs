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
                url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
            }}
            @font-face {{
                font-family: 'Source Sans Pro';
                font-style: normal;
                font-weight: 700;
                src: local('Source Sans Pro Bold'),
                url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
            }}
            }}
          
            body, table, td, a {{
            -ms-text-size-adjust: 100%;
            -webkit-text-size-adjust: 100%;
            }}

            table, td {{
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
            background-color: #f3f4f6;
            }}

            table {{
            border-collapse: collapse !important;
            }}

            a {{
            color: #1a82e2;
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
            padding: 20px;
            }}

            .email-header {{
            background-color: #ffffff;
            padding: 40px 20px;
            border-radius: 8px 8px 0 0;
            text-align: center;
            border-bottom: 3px solid #d4dadf;
            }}

            .email-header img {{
            width: 90px;
            }}

            .email-body {{
            background-color: #ffffff;
            padding: 40px 20px;
            border-radius: 0 0 8px 8px;
            }}

            .email-body h1 {{
            font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
            font-size: 28px;
            font-weight: 700;
            margin: 0;
            color: #333333;
            }}

            .email-body p {{
            font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
            font-size: 16px;
            line-height: 24px;
            margin: 20px 0;
            color: #555555;
            }}

            .email-button {{
            background-color: #1a82e2;
            border-radius: 6px;
            text-align: center;
            margin-top: 20px;
            }}

            .email-button a {{
            display: inline-block;
            padding: 16px 36px;
            font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
            font-size: 16px;
            color: #ffffff;
            text-decoration: none;
            border-radius: 6px;
            font-weight: 700;
            }}

            .email-footer {{
            padding: 20px;
            text-align: center;
            font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif;
            font-size: 14px;
            line-height: 20px;
            color: #888888;
            }}
        </style>
        </head>
        <body>
        <div class='email-container'>
            <div class='email-header'>
                <img
                src='https://res.cloudinary.com/df6ylojjq/image/upload/v1726342908/cgizvbzhz7ere26nibno.png'
                alt='SyncSpace Logo'
                />
            </a>
            </div>

            <div class='email-body'>
            <h1>Confirm Your Email Address</h1>
            <p>Click the button below to confirm your email address. If you did not create an account, you can safely ignore this email.</p>
            <div class='email-button'>
                <a href='{Url}' target='_blank'>Confirm Email</a>
            </div>
            <p>If that doesn't work, copy and paste the following link in your browser:</p>
            <p><a href='{Url}' target='_blank'>{Url}</a></p>
            </div>

            <div class='email-footer'>
            <p>You received this email because we received a request for SyncSpace for your account. If you didn't request this, you can safely delete this email.</p>
            <p>Cheers,<br />SyncSpace Team</p>
            </div>
        </div>
        </body>
    </html>
    ";

    }
}
