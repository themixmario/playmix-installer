# Creates the key that signs the APK. Keep playmix.keystore and the password safe: losing them means
# future APKs cannot update the ones already installed.
# Needs Java (keytool). Then add three GitHub secrets (repo > Settings > Secrets and variables > Actions):
#   ANDROID_KEYSTORE_BASE64   the text copied to the clipboard at the end
#   ANDROID_KEYSTORE_PASSWORD the password you type below
#   ANDROID_KEY_ALIAS         playmix
param([Parameter(Mandatory)][string]$Password)
$ks = Join-Path $PWD "playmix.keystore"
keytool -genkeypair -v -keystore $ks -alias playmix -keyalg RSA -keysize 2048 -validity 10000 -storepass $Password -keypass $Password -dname "CN=PlayMix Installer, O=Valtecno, C=CL"
$b64 = [Convert]::ToBase64String([IO.File]::ReadAllBytes($ks))
Set-Clipboard $b64
Write-Host "Done. ANDROID_KEYSTORE_BASE64 is on your clipboard (paste it as the secret)." -ForegroundColor Green
