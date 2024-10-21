import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { StoryModel } from '../models/domain/story-model';

@Component({
  selector: 'app-story-search-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './story-search-item.component.html',
  styleUrl: './story-search-item.component.scss'
})
export class SearchStoryItemComponent {
  @Input() story: StoryModel;

  ngOnInit(): void {
  this.story.Description = `fdsdf
  dfasf dfasf dfasfdfasfdfasf dfasf dfg oskdfgosdk fiogks dfogk fsdsdfg,.g sdm,fgoskio0fg hkm f,gso dfkgisk dfogkfis fgoaskdkfiadskdfo
  dfasf dfasf dfasfdfasfdfasf dfasf dfg oskdfgosdk fiogks dfogk fsdsdfg,.g sdm,fgoskio0fg hkm f,gso dfkgisk dfogkfis fgoaskdkfiadskdfo
  dfasf dfasf dfasfdfasfdfasf dfasf dfg oskdfgosdk fiogks dfogk fsdsdfg,.g sdm,fgoskio0fg hkm f,gso dfkgisk dfogkfis fgoaskdkfiadskdfo
  dfasf dfasf dfasfdfasfdfasf dfasf dfg oskdfgosdk fiogks dfogk fsdsdfg,.g sdm,fgoskio0fg hkm f,gso dfkgisk dfogkfis fgoaskdkfiadskdfo
  dfasf dfasf dfasfdfasfdfasf dfasf dfg oskdfgosdk fiogks dfogk fsdsdfg,.g sdm,fgoskio0fg hkm f,gso dfkgisk dfogkfis fgoaskdkfiadskdfo`
  }
}
