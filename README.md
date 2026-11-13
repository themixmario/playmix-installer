# PlayMix2Samsung

Android app that installs **PlayMix TV** on Samsung Smart TVs (Tizen 3.0 / 2017 or newer): it finds the TV, creates the signing certificate, and installs the latest PlayMix TV package. No PC and no Tizen Studio.

## How it works
- The catalog (`third-party-apps.json`) lists a single app: the `.wgt` of the latest release of `themixmario/playmix-tv-samsung` (the package may be unsigned: the installer signs it for your TV).
- The first install asks you to sign in with a Samsung account to create the certificate. The TV needs **Developer Mode** on.

## Build
Actions > **Build APK** > Run workflow, or push a tag `v1.0.0` to also get a GitHub release. To sign with your own key, run `tools/make-keystore.ps1` and add the three secrets described there.

## Credits and license
Based on [Apps2Samsung](https://github.com/Apps2Samsung/Apps2Samsung) by Patrick Stel and contributors, licensed **GPL-3.0**. This fork is distributed under the same license (see `LICENSE` and `NOTICE.md`); its source code is this repository. It is not the official Apps2Samsung.
