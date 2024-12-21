import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, timer } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ImageService } from '../services/image.service';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';
import { SettingsService } from 'src/app/settings/services/settings.service';

@Component({
  selector: 'app-background',
  templateUrl: './background.component.html',
  styleUrls: ['./background.component.css']
})
export class BackgroundComponent implements OnInit, OnDestroy {

  image_1_url: string;
  image_2_url: string;
  firstImageActive: boolean = true;
  setFirstImage: boolean = true;
  partialHeight: boolean = false;
  settingsLoaded: boolean = false;

  private _unsubscribe: Subject<void> = new Subject();
  private _rotationInterval: number = (environment.intervals.image * 1000);

  constructor(private _imageService: ImageService, private _settingsService : SettingsService
    , private _sanitizer: DomSanitizer) {
  }
  ngOnInit() {
    this.loadSettings();
    this.getImage();
  }

  loadSettings() : void {
    var setting = this._settingsService.getSetting("CalendarLocation", "full");
    this.partialHeight = setting[0] == "bottom";
    this.settingsLoaded = setting[1];
    if(!this.settingsLoaded){
      setTimeout(() => {
        this.loadSettings();
      }, 1000);
    }
  }

  getImage(): void {
    this.image_1_url = "./assets/default.jpg";
    this.image_2_url = "./assets/default.jpg";
    this.setupImageRotation()
  }

  setupImageRotation(): void {

    timer(0,this._rotationInterval).pipe(
      takeUntil(this._unsubscribe)
    ).subscribe(() => {
      this._imageService.getImageUrl()
        .subscribe(result => {
            if(this.firstImageActive){
              this.image_2_url = result;
            }else{
              this.image_1_url = result;
            }
            setTimeout(() => {
              this.firstImageActive = !this.firstImageActive;
            }, 3000);
        });
    });
  }

  getSafeUrl(url: string): SafeStyle {
    return this._sanitizer.bypassSecurityTrustStyle(`url(${url})`);
  }

  ngOnDestroy(): void {
    this._unsubscribe.next();
    this._unsubscribe.complete();
  }
}
