import { MatchFilter } from './../proxy/matchs/models';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatchService, MatchDto, CreateUpdateMatchDto } from '@proxy/matchs';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.scss'],
  providers: [ListService],
})
export class MatchComponent implements OnInit  {
  data = { items: [], totalCount: 0 } as PagedResultDto<MatchDto>;
  form: FormGroup;
  selected = {} as MatchDto;
  isModalOpen = false;
  filter: MatchFilter = {};
  filterHideShow = false;

  constructor(public readonly list: ListService,
    private service: MatchService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private toast: ToasterService) {}

  ngOnInit() {
    this.getData();
  }

  getData(){
    const streamCreator = (query) => this.service.getListByFilter(query, this.filter);
    this.list.hookToQuery(streamCreator).subscribe((response) => {
      this.data = response;
    });
  }

  toggleFilter(){
    this.filterHideShow = !this.filterHideShow;
  }

  create() {
    this.selected = {} as MatchDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  edit(id: string) {
    this.service.get(id).subscribe((data) => {
      this.selected = data;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  buildForm() {
    this.form = this.fb.group({
      			tournamentId: [this.selected.tournamentId, Validators.required],
			groupId: [this.selected.groupId],
			teamAId: [this.selected.teamAId, Validators.required],
			teamBId: [this.selected.teamBId, Validators.required],
			matchDate: [this.selected.matchDate, Validators.required],
			teamAScore: [this.selected.teamAScore],
			teamBScore: [this.selected.teamBScore],

    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }
    const dto: CreateUpdateMatchDto = {
      			tournamentId: this.form.get("tournamentId").value,
			groupId: this.form.get("groupId").value,
			teamAId: this.form.get("teamAId").value,
			teamBId: this.form.get("teamBId").value,
			matchDate: this.form.get("matchDate").value,
			teamAScore: this.form.get("teamAScore").value,
			teamBScore: this.form.get("teamBScore").value,

    };
    const request = this.selected.id
      ? this.service.update(this.selected.id, dto)
      : this.service.create(dto);

    request.subscribe(() => {
      this.toast.success(this.selected.id?'::MatchUpdated':'::MatchCreated', "::SUCCESS", {
        tapToDismiss: true,
        life: 2500
      });
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  delete(id: string) {
    this.confirmation.warn('::PressOKToContinue', 'AbpAccount::AreYouSure').subscribe((status) => {
      if (status === Confirmation.Status.confirm) {
        this.service.delete(id).subscribe(() => {
          this.toast.success('::MatchDeleted', "::SUCCESS", {
            tapToDismiss: true,
            life: 2500
          });
          this.list.get();
        });
      }
    });
  }
}
