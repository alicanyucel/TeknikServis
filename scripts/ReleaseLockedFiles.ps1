# Attempts to find and kill processes that have loaded TeknikServis.WebAPI.dll to release locked bin files
Try {
    $targetAssembly = 'TeknikServis.WebAPI.dll'
    $processes = Get-Process -ErrorAction SilentlyContinue
    foreach ($p in $processes) {
        try {
            if ($p.Modules) {
                foreach ($m in $p.Modules) {
                    if ($m.FileName -and $m.FileName.EndsWith($targetAssembly, [System.StringComparison]::InvariantCultureIgnoreCase)) {
                        Write-Host "Killing process $($p.ProcessName) (Id: $($p.Id)) which has loaded $targetAssembly"
                        $p.Kill()
                        Start-Sleep -Milliseconds 200
                        break
                    }
                }
            }
            elseif ($p.ProcessName -like '*TeknikServis.WebAPI*') {
                Write-Host "Killing process $($p.ProcessName) (Id: $($p.Id)) by name match"
                $p.Kill()
                Start-Sleep -Milliseconds 200
            }
        } catch {
            # ignore access denied or other errors
        }
    }
} catch {
    # ignore any errors
}
