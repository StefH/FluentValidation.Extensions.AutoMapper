rem https://github.com/StefH/GitHubReleaseNotes

SET version=11.0.0

GitHubReleaseNotes --output ReleaseNotes.md --skip-empty-releases --exclude-labels question invalid documentation --version %version%