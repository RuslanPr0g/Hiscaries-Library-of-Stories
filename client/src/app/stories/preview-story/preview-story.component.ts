import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StoryModel } from '../models/domain/story-model';
import { take } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormButtonComponent } from '../../shared/components/form-button/form-button.component';
import { NavigationConst } from '../../shared/constants/navigation.const';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { StoryWithMetadataService } from '../../user-to-story/services/multiple-services-merged/story-with-metadata.service';

@Component({
    selector: 'app-preview-story',
    standalone: true,
    imports: [CommonModule, FormButtonComponent, ProgressSpinnerModule],
    templateUrl: './preview-story.component.html',
    styleUrl: './preview-story.component.scss',
})
export class PreviewStoryComponent implements OnInit {
    private storyId: string | null = null;

    story: StoryModel | null = null;
    storyNotFound = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private storyService: StoryWithMetadataService
    ) {}

    ngOnInit(): void {
        this.storyId = this.route.snapshot.paramMap.get('id');

        this.storyService
            .searchStory({
                Id: this.storyId,
            })
            .pipe(take(1))
            .subscribe((stories) => {
                const story = stories[0];
                if (!story) {
                    this.storyNotFound = true;
                } else {
                    this.story = story;
                }
            });
    }

    get backgroundImageUrl(): string | undefined {
        return this.story?.ImagePreviewUrl;
    }

    get isEditable(): boolean {
        return this.story?.IsEditable ?? false;
    }

    readStory(): void {
        this.router.navigate([NavigationConst.ReadStory(this.storyId!)]);
    }

    modifyStory(): void {
        this.router.navigate([NavigationConst.ModifyStory(this.storyId!)]);
    }

    navigateToLibrary(): void {
        if (this.story?.LibraryId) {
            this.router.navigate([NavigationConst.PublisherLibrary(this.story?.LibraryId)]);
        } else {
            console.warn('Why the story library id is empty?');
        }
    }
}
