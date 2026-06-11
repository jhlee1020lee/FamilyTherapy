param(
    [Parameter(Mandatory = $true)]
    [string]$RuntimeName,

    [Parameter(Mandatory = $true)]
    [string]$SourceName,

    [string]$RuntimeDir = "Assets\Resources\VN\EventCG\FT002",
    [string]$SourceDir = "Docs\GeneratedSources\FT002_V3_CG_20260610\source\batch02",
    [int]$Minutes = 15
)

$ErrorActionPreference = "Stop"

$generatedRoot = Join-Path $env:USERPROFILE ".codex\generated_images"
$src = Get-ChildItem -LiteralPath $generatedRoot -Recurse -File -Filter *.png |
    Where-Object { $_.LastWriteTime -gt (Get-Date).AddMinutes(-$Minutes) } |
    Sort-Object LastWriteTime -Descending |
    Select-Object -First 1

if (-not $src) {
    throw "No generated PNG found under $generatedRoot in the last $Minutes minutes."
}

New-Item -ItemType Directory -Force -Path $RuntimeDir, $SourceDir | Out-Null
Copy-Item -LiteralPath $src.FullName -Destination (Join-Path $SourceDir $SourceName) -Force

Add-Type -AssemblyName System.Drawing

$img = [System.Drawing.Image]::FromFile($src.FullName)
try {
    $targetW = 1600
    $targetH = 900
    $targetRatio = $targetW / $targetH
    $srcRatio = $img.Width / $img.Height

    if ($srcRatio -gt $targetRatio) {
        $cropH = $img.Height
        $cropW = [int]($cropH * $targetRatio)
        $cropX = [int](($img.Width - $cropW) / 2)
        $cropY = 0
    } else {
        $cropW = $img.Width
        $cropH = [int]($cropW / $targetRatio)
        $cropX = 0
        $cropY = [int](($img.Height - $cropH) / 2)
    }

    $destBmp = New-Object System.Drawing.Bitmap $targetW, $targetH
    try {
        $g = [System.Drawing.Graphics]::FromImage($destBmp)
        try {
            $g.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
            $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
            $g.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
            $targetRect = New-Object System.Drawing.Rectangle 0, 0, $targetW, $targetH
            $sourceRect = New-Object System.Drawing.Rectangle $cropX, $cropY, $cropW, $cropH
            $g.DrawImage($img, $targetRect, $sourceRect, [System.Drawing.GraphicsUnit]::Pixel)
        } finally {
            $g.Dispose()
        }

        $outPath = Join-Path (Resolve-Path -LiteralPath $RuntimeDir).Path $RuntimeName
        $destBmp.Save($outPath, [System.Drawing.Imaging.ImageFormat]::Png)
    } finally {
        $destBmp.Dispose()
    }
} finally {
    $img.Dispose()
}

$check = [System.Drawing.Image]::FromFile((Join-Path (Resolve-Path -LiteralPath $RuntimeDir).Path $RuntimeName))
try {
    [pscustomobject]@{
        Source = $src.FullName
        SourceCopy = (Resolve-Path -LiteralPath (Join-Path $SourceDir $SourceName)).Path
        Output = (Resolve-Path -LiteralPath (Join-Path $RuntimeDir $RuntimeName)).Path
        Width = $check.Width
        Height = $check.Height
    }
} finally {
    $check.Dispose()
}
